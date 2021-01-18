using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Domain.Base;
using Monitor.Infrastructure;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public abstract class BaseVisitSiteManageService<TViewModel, TEntity> : IBaseVisitSiteManageService<TViewModel>
        where TViewModel : BaseVisiteSiteViewModel
        where TEntity : class, IVisitEntity
    {
        protected readonly IBaseRepository Repository;

        public BaseVisitSiteManageService(IBaseRepository repository)
        {
            Repository = repository;
        }

        public async virtual Task<IList<VisitLightModel>> GetAll(int siteId)
        {
            using (Repository)
            {
                return await Repository.GetQuery<TEntity>(x => x.SiteId == siteId)
                    .GroupBy(x => x.VisitDate)
                    .Select(x => new VisitLightModel
                    {
                        VisitDate = x.Key,
                        LastModified = x.Max(y => y.Modified ?? y.Created)
                    }).OrderByDescending(z => z.VisitDate)
                    .ToListAsync();
            }
        }

        public abstract Task<TViewModel> Get(int siteId, DateTime date);

        public abstract Task<TViewModel> Save(int siteId, TViewModel model);

        protected virtual async Task<TViewModel> GetViewModel(int siteId, DateTime visitDate)
            => await Repository.Get<TViewModel, TEntity>(x => x.VisitDate == visitDate && x.SiteId == siteId);
    }
}
