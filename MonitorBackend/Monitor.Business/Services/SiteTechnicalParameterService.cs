using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public class SiteTechnicalParameterService : ISiteTechnicalParameterService
    {
        private readonly IBaseRepository _repository;

        public SiteTechnicalParameterService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<SiteTechParameterViewModel> Get(int siteId)
        {
            using (_repository)
            {
                return await _repository.Get<SiteTechParameterViewModel, SiteTechParameter>(x => x.SiteId == siteId);
            }
        }

        public async Task<SiteTechParameterViewModel> Save(int siteId, SiteTechParameterViewModel model)
        {
            using (_repository)
            {
                model.IsValid();

                var entity = await _repository.GetQuery<SiteTechParameter>(x => x.SiteId == siteId, true)
                    .FirstOrDefaultAsync();

                entity ??= new SiteTechParameter(siteId);

                entity.SetRenewableCapacity(model.RenewableTechnology, model.RenewableCapacity);
                entity.SetGrid(model.GridConnection, model.GridLength);
                entity.SetStorageCapacity(model.StorageTechnology, model.StorageCapacity);
                entity.SetConventionalCapacity(model.ConventionalTechnology, model.ConventionalCapacity);
                entity.SetManufacturer(model.InverterManufacturer, model.MeterManufacturer, model.MeteringTypeId);

                if (entity.Id == 0)
                {
                    await _repository.Add(entity);
                }

                await _repository.SaveChanges();

                return _repository.Mapper.Map<SiteTechParameterViewModel>(entity);
            }
        }
    }
}
