using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Monitor.Common;
using Monitor.Infrastructure;

namespace Monitor.HostedServices
{
    public abstract class BaseHostedService : BackgroundService
    {
        protected readonly Logger Logger;
        protected readonly string ServiceName;
        protected readonly string ConnectionString;
        private readonly int _delay;

        public BaseHostedService(IConfiguration configuration, ServiceType type)
        {
            Logger = LogManager.GetCurrentClassLogger();
            ServiceName = $"{type.ToString().Substring(0, 1)}{type.ToString().Substring(1).ToLower()}";
            ConnectionString = configuration[Constants.CONFIG_CONNECTION_STRING];

            _delay = int.Parse(configuration[$"Intervals:{ServiceName}"]);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() => Logger.Debug($"{ServiceName} background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                await ExecuteWorkerAsync();

                await WaitForNextRun(_delay, stoppingToken);
            }
        }

        protected abstract Task ExecuteWorkerAsync();

        private async Task WaitForNextRun(int interval, CancellationToken stoppingToken)
        {
            var endTime = DateTime.UtcNow.AddMinutes(interval);

            while (DateTime.UtcNow < endTime && !stoppingToken.IsCancellationRequested)
            {
                await Task.Delay((int)TimeSpan.FromSeconds(5).TotalMilliseconds);
            }
        }

        protected BaseRepository GetRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MinigridDbContext>();
            optionsBuilder.UseNpgsql(ConnectionString, x => x.UseNetTopologySuite());

            return new BaseRepository(new MinigridDbContext(optionsBuilder.Options));
        }
    }
}
