using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Domain.Base;
using Monitor.Common.Enums;
using Monitor.Business.Services;
using Monitor.Domain.LightModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Base Manage Controller
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    /// <typeparam name="TService"></typeparam>
    [ValidateUserRole(new[] { RoleCode.DEVELOPER, RoleCode.PROGRAMME_MANAGER }, Order = 1)]
    [CanManageSite("siteId", Order = 2)]
    public abstract class BaseVisitSiteManageController<TViewModel, TService> : AuthorizeController
        where TViewModel : BaseVisiteSiteViewModel
        where TService : IBaseVisitSiteManageService<TViewModel>
    {
        /// <summary>
        /// Base Manage Service
        /// </summary>
        protected readonly TService Service;

        /// <summary>
        /// Ctr
        /// </summary>
        public BaseVisitSiteManageController(TService service)
        {
            Service = service;
        }

        /// <summary>
        /// GetAll
        /// </summary>
        [HttpGet("{siteId}")]
        public virtual async Task<IList<VisitLightModel>> GetAll(int siteId)
            => await Service.GetAll(siteId);

        /// <summary>
        /// Get
        /// </summary>
        [HttpPost("{siteId}")]
        public virtual async Task<TViewModel> Get(int siteId, [FromBody] VisitLightModel data)
            => await Service.Get(siteId, data.VisitDate);

        /// <summary>
        /// Save
        /// </summary>
        [HttpPatch("{siteId}")]
        public virtual async Task<TViewModel> Save(int siteId, TViewModel model)
            => await Service.Save(siteId, model);
    }
}