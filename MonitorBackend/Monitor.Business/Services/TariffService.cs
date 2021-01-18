using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public class TariffService : BaseVisitSiteManageService<TariffViewModel, Tariff>, ITariffService
    {
        public TariffService(IBaseRepository repository)
            : base(repository)
        { }

        public override async Task<TariffViewModel> Get(int siteId, DateTime date)
        {
            using (Repository)
            {
                return await GetViewModel(siteId, date);
            }
        }

        public override async Task<TariffViewModel> Save(int siteId, TariffViewModel model)
        {
            using (Repository)
            {
                var entity = await Repository.GetQuery<Tariff>(x => x.VisitDate == model.VisitDate && x.SiteId == siteId, true)
                    .SingleOrDefaultAsync();

                entity ??= new Tariff(siteId, model.VisitDate);
                entity.Set(model.Residential, model.Commercial, model.Public, model.Productive);

                if (entity.Id == 0)
                {
                    await Repository.Add(entity);
                }

                await Repository.SaveChanges();

                return await GetViewModel(siteId, model.VisitDate);
            }
        }
    }
}
