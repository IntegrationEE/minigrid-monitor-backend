using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public class PeopleConnectedService : BaseVisitSiteManageService<PeopleConnectedViewModel, PeopleConnected>, IPeopleConnectedService
    {
        public PeopleConnectedService(IBaseRepository repository)
            : base(repository)
        { }

        public override async Task<PeopleConnectedViewModel> Get(int siteId, DateTime date)
        {
            using (Repository)
            {
                return await GetViewModel(siteId, date);
            }
        }

        public override async Task<PeopleConnectedViewModel> Save(int siteId, PeopleConnectedViewModel model)
        {
            using (Repository)
            {
                var entity = await Repository.GetQuery<PeopleConnected>(x => x.VisitDate == model.VisitDate && x.SiteId == siteId, true)
                    .SingleOrDefaultAsync();

                entity ??= new PeopleConnected(siteId, model.VisitDate);
                entity.Set(model.Productive, model.Commercial, model.Residential, model.Public);

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
