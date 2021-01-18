using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Domain.Base;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public interface IBaseVisitSiteManageService<TViewModel>
        where TViewModel : BaseVisiteSiteViewModel
    {
        Task<IList<VisitLightModel>> GetAll(int siteId);
        Task<TViewModel> Get(int siteId, DateTime date);
        Task<TViewModel> Save(int siteId, TViewModel model);
    }
}