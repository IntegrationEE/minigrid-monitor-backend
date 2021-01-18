using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public class IntegrationRecordService : IIntegrationRecordService
    {
        private readonly IBaseRepository _repository;

        public IntegrationRecordService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<IntegrationRecordLightModel>> GetAll(int integrationId)
        {
            using (_repository)
            {
                return await _repository.GetQuery<IntegrationRecord>(z => z.IntegrationId == integrationId)
                    .OrderByDescending(z => z.Created)
                    .ProjectTo<IntegrationRecordLightModel>(_repository.Mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }
    }
}
