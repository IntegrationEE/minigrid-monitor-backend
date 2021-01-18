using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public class FinanceCapexService : IFinanceCapexService
    {
        private readonly IBaseRepository _repository;

        public FinanceCapexService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<FinanceCapexViewModel> Get(int siteId)
        {
            using (_repository)
            {
                return await _repository.Get<FinanceCapexViewModel, FinanceCapex>(x => x.SiteId == siteId);
            }
        }

        public async Task<FinanceCapexViewModel> Save(int siteId, FinanceCapexViewModel model)
        {
            using (_repository)
            {
                var entity = await _repository.GetQuery<FinanceCapex>(x => x.SiteId == siteId, true)
                    .FirstOrDefaultAsync();

                entity ??= new FinanceCapex(siteId);

                entity.Set(model.Generation, model.SiteDevelopment, model.Logistics, model.Distribution, model.CustomerInstallation,
                    model.Commissioning, model.Taxes);
                entity.SetFinancing(model.FinancingGrant, model.FinancingEquity, model.FinancingDebt);

                if (entity.Id == 0)
                {
                    await _repository.Add(entity);
                }

                await _repository.SaveChanges();

                return _repository.Mapper.Map<FinanceCapexViewModel>(entity);
            }
        }
    }
}
