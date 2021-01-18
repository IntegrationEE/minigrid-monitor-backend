using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public class NewServiceService : BaseVisitSiteManageService<NewServiceViewModel, NewService>, INewServiceService
    {
        public NewServiceService(IBaseRepository repository)
            : base(repository)
        { }

        public override async Task<NewServiceViewModel> Get(int siteId, DateTime date)
        {
            using (Repository)
            {
                return await GetViewModel(siteId, date);
            }
        }

        public override async Task<NewServiceViewModel> Save(int siteId, NewServiceViewModel model)
        {
            using (Repository)
            {
                var entity = await Repository.GetQuery<NewService>(x => x.VisitDate == model.VisitDate && x.SiteId == siteId, true)
                    .SingleOrDefaultAsync();

                entity ??= new NewService(siteId, model.VisitDate);
                entity.Set(model.Commercial, model.Productive, model.Health, model.Education);

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
