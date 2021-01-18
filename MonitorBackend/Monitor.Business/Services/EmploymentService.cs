using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public class EmploymentService : BaseVisitSiteManageService<EmploymentViewModel, Employment>, IEmploymentService
    {
        public EmploymentService(IBaseRepository repository)
            : base(repository)
        { }

        public override async Task<EmploymentViewModel> Get(int siteId, DateTime date)
        {
            using (Repository)
            {
                return await GetViewModel(siteId, date);
            }
        }

        public override async Task<EmploymentViewModel> Save(int siteId, EmploymentViewModel model)
        {
            using (Repository)
            {
                var entity = await Repository.GetQuery<Employment>(x => x.VisitDate == model.VisitDate && x.SiteId == siteId, true)
                    .SingleOrDefaultAsync();

                entity ??= new Employment(siteId, model.VisitDate);
                entity.Set(model.Direct, model.Indirect);

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
