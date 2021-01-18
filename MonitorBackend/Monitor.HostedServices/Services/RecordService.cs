using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitor.Common.Enums;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.HostedServices.Models;

namespace Monitor.HostedServices
{
    internal class RecordService
    {
        private readonly IBaseRepository _repository;

        public RecordService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> GetLatestOrCreate(int integrationId, string value)
        {
            var entity = await _repository.GetQuery<IntegrationRecord>(z => z.IntegrationId == integrationId && z.Status == IntegrationStatusCode.RUN_CREATED)
                .SingleOrDefaultAsync();

            if (entity != null)
                return entity.Id;

            entity = new IntegrationRecord(integrationId, value, IntegrationStatusCode.RUN_CREATED);

            await _repository.Add(entity);
            await _repository.SaveChanges();

            return entity.Id;
        }

        public async Task SetStatus(int id, RecordStatusModel model)
        {
            var entity = await _repository.GetQuery<IntegrationRecord>(z => z.Id == id, true)
                .FirstAsync();

            entity.SetStatus(model.Status, model.Inserted, model.Error, model.StepId, model.EndDate);

            await _repository.SaveChanges();
        }
    }
}
