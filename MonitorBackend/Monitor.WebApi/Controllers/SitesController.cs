using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Business.Services;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Sites Controller
    /// </summary>
    public class SitesController : AuthorizeController
    {
        private readonly ISiteService _service;
        private readonly ISiteStatusService _statusService;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        /// <param name="statusService"></param>
        public SitesController(ISiteService service, ISiteStatusService statusService)
        {
            _service = service;
            _statusService = statusService;
        }
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ValidateUserRole(new RoleCode[] { RoleCode.DEVELOPER, RoleCode.PROGRAMME_MANAGER })]
        public async Task<IList<SiteCardModel>> GetByCurrent()
            => await _service.GetByCurrent(CurrentUser);

        /// <summary>
        /// Get list
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IList<SiteLightModel>> GetList()
           => await _service.GetList(CurrentUser);

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ValidateUserRole(new RoleCode[] { RoleCode.DEVELOPER, RoleCode.PROGRAMME_MANAGER }, Order = 1)]
        [CanManageSite("id", Order = 2)]
        public async Task<SiteViewModel> Get(int id)
            => await _service.Get(id);

        /// <summary>
        /// Get site status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/status")]
        [ValidateUserRole(roles: new RoleCode[] { RoleCode.DEVELOPER, RoleCode.PROGRAMME_MANAGER }, Order = 1)]
        [CanManageSite("id", Order = 2)]
        public async Task<SiteStatusModel> GetStatus(int id)
            => await _statusService.GetStatus(id);

        /// <summary>
        /// Get Sites based on Filter
        /// </summary>
        /// <param name="filters"></param>
        /// <returns>All Sites that match the provided filter</returns>
        [HttpPost("filters")]
        public async Task<IList<SiteDashboardModel>> List([FromBody] FilterParametersViewModel filters)
           => await _service.GetFilteredSite(filters);

        /// <summary>
        /// Create Site
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateUserRole(new RoleCode[] { RoleCode.DEVELOPER, RoleCode.PROGRAMME_MANAGER })]
        public async Task<SiteViewModel> Post([FromBody] SiteViewModel model)
        {
            model.CompanyId = CurrentCompanyId.Value;

            return await _service.Create(model);
        }
        /// <summary>
        /// Update Site
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ValidateUserRole(new RoleCode[] { RoleCode.DEVELOPER, RoleCode.PROGRAMME_MANAGER }, Order = 1)]
        [CanManageSite("id", Order = 2)]
        public async Task<SiteViewModel> Patch(int id, [FromBody] SiteViewModel model)
            => await _service.Update(id, model);

        /// <summary>
        /// Toggle Is Published
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}/published")]
        [ValidateUserRole(new RoleCode[] { RoleCode.DEVELOPER, RoleCode.PROGRAMME_MANAGER }, Order = 1)]
        [CanManageSite("id", Order = 2)]
        public async Task ToggleIsPublished(int id)
            => await _service.ToggleIsPublished(id);

        /// <summary>
        /// Delete Site
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ValidateUserRole(RoleCode.DEVELOPER, Order = 1)]
        [CanManageSite("id", Order = 2)]
        public async Task Delete(int id)
            => await _service.Delete(id);
    }
}
