using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monitor.Common.Enums;
using Monitor.Domain.ViewModels;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Site Technical Parameters Controller
    /// </summary>
    [ValidateUserRole(new RoleCode[] { RoleCode.DEVELOPER, RoleCode.PROGRAMME_MANAGER }, Order = 1)]
    [CanManageSite("siteId", Order = 2)]
    public class SiteTechnicalParametersController : AuthorizeController
    {
        private readonly ISiteTechnicalParameterService _service;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public SiteTechnicalParametersController(ISiteTechnicalParameterService service)
        {
            _service = service;
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        [HttpGet("{siteId}")]
        public async Task<SiteTechParameterViewModel> Get(int siteId)
            => await _service.Get(siteId);
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{siteId}")]
        public async Task<SiteTechParameterViewModel> Save(int siteId, [FromBody] SiteTechParameterViewModel model)
            => await _service.Save(siteId, model);
    }
}