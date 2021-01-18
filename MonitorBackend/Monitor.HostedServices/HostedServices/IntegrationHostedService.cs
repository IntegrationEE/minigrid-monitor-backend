using Npgsql;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Domain.ViewModels;
using Monitor.HostedServices.Models;

namespace Monitor.HostedServices
{
    public class IntegrationHostedService : BaseHostedService, IIntegrationHostedService
    {
        private Dictionary<int, TaskEntry> TaskEntries { get; set; }
        private readonly object _lock = new object();

        public IntegrationHostedService(IConfiguration configuration)
            : base(configuration, ServiceType.INTEGRATION)
        {
            TaskEntries = new Dictionary<int, TaskEntry>();
        }

        public async Task Start()
        {
            await StartAsync(new CancellationToken());
        }

        public void Remove(int integrationId, int userId)
        {
            lock (_lock)
            {
                var entry = TaskEntries.GetValueOrDefault(integrationId);

                if (entry != null)
                {
                    entry.SetStatus(TaskStatus.REMOVED);
                    entry.CancellationTokenSource.Cancel();

                    Task.Delay(500).Wait();

                    TaskEntries.Remove(integrationId);

                    Logger.Debug($"{ServiceName} with ID: '{integrationId}' was stopped by User '{userId}'.");
                }
            }
        }

        public void AddNew(IntegrationViewModel integration, int userId)
        {
            lock (_lock)
            {
                if (!TaskEntries.Any(z => z.Key == integration.Id))
                {
                    var model = new IntegrationModel
                    {
                        Id = integration.Id,
                        QuestionHash = integration.QuestionHash,
                        Interval = integration.Interval,
                        Token = integration.Token,
                        Steps = integration.Steps
                            .OrderBy(z => z.Ordinal)
                            .Select(z => new StepModel
                            {
                                Function = z.Function,
                                Ordinal = z.Ordinal,
                                Id = z.Id,
                                Name = z.Name
                            })
                    };

                    TaskEntries.Add(integration.Id, new TaskEntry(Task.Run(() => ExecuteTask(model))));

                    Logger.Debug($"{ServiceName} with ID: '{integration.Id}' was added by User '{userId}'.");
                }
            }
        }

        public void Restart(int integrationId, int userId)
        {
            lock (_lock)
            {
                var entry = TaskEntries.GetValueOrDefault(integrationId);

                if (entry != null)
                {
                    entry.SetStatus(TaskStatus.RESTARTED);
                    entry.CancellationTokenSource.Cancel();

                    Logger.Debug($"{ServiceName} with ID: '{integrationId}' was restarted by User '{userId}'.");
                }
            }
        }

        protected override async Task ExecuteWorkerAsync()
        {
            using (var repository = GetRepository())
            {
                var service = new IntegrationService(repository);

                var integrations = await service.GetAll();

                lock (_lock)
                {
                    TaskEntries.Clear();

                    foreach (var item in integrations)
                    {
                        TaskEntries.Add(item.Id, new TaskEntry(Task.Run(() => ExecuteTask(item))));
                    }
                }
            }

            await Task.WhenAll(TaskEntries.Values.Select(z => z.Task));
        }

        private async Task ExecuteTask(IntegrationModel integration)
        {
            var entry = TaskEntries.GetValueOrDefault(integration.Id);
            entry.SetStatus(TaskStatus.EXECUTING);

            while (!entry.CancellationTokenSource.IsCancellationRequested)
            {
                entry.SetRecordId(null);

                using (var repository = GetRepository())
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    var recordService = new RecordService(repository);
                    int? stepId = null;

                    try
                    {
                        var api = new KoboToolboxApi(integration.QuestionHash, integration.Token);

                        var results = await api.GetData();

                        if (!string.IsNullOrWhiteSpace(results))
                        {
                            entry.SetRecordId(await recordService.GetLatestOrCreate(integration.Id, results));

                            int inserted = 0;

                            await connection.OpenAsync();

                            foreach (var step in integration.Steps)
                            {
                                stepId = step.Id;

                                if (entry.CancellationTokenSource.IsCancellationRequested)
                                {
                                    throw new CustomException($"Step '{step.Name}' ({step.Ordinal}) was canceled");
                                }

                                var stepCommmand = new NpgsqlCommand($"SELECT * FROM {step.Function}({entry.RecordId.Value})", connection);

                                using var reader = await stepCommmand.ExecuteReaderAsync();

                                while (reader.Read())
                                {
                                    inserted = int.Parse(reader[0].ToString());
                                }

                                await recordService.SetStatus(entry.RecordId.Value, new RecordStatusModel
                                {
                                    Status = IntegrationStatusCode.STEP_COMPLETED,
                                    StepId = stepId,
                                });
                            }

                            await connection.CloseAsync();

                            await recordService.SetStatus(entry.RecordId.Value, new RecordStatusModel
                            {
                                Status = IntegrationStatusCode.RUN_COMPLETED,
                                Inserted = inserted,
                                EndDate = DateTime.UtcNow,
                                StepId = stepId,
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);

                        if (entry.RecordId.HasValue)
                        {
                            await recordService.SetStatus(entry.RecordId.Value, new RecordStatusModel
                            {
                                Status = IntegrationStatusCode.STEP_CANCELED,
                                EndDate = DateTime.UtcNow,
                                Error = ex.Message,
                                StepId = stepId
                            });
                        }
                    }
                }

                await WaitForNextRun(integration.Interval, entry.CancellationTokenSource);
            }

            if (entry.Status == TaskStatus.RESTARTED)
            {
                entry.CreateNewToken();
                await ExecuteTask(integration);
            }
        }

        private async Task WaitForNextRun(int interval, CancellationTokenSource cancellationTokenSource)
        {
            var endTime = DateTime.UtcNow.AddDays(interval);

            while (DateTime.UtcNow < endTime && !cancellationTokenSource.IsCancellationRequested)
            {
                await Task.Delay((int)TimeSpan.FromSeconds(5).TotalMilliseconds);
            }
        }
    }
}
