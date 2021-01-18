using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Common.Enums;
using Monitor.Business.Services;
using Monitor.Domain.ViewModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Thresholds Controller
    /// </summary>
    public class ThresholdsController : AuthorizeController
    {
        private readonly IThresholdService _service;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public ThresholdsController(IThresholdService service)
        {
            _service = service;
        }
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ValidateUserRole(new RoleCode[] { RoleCode.ADMINISTRATOR, RoleCode.DEVELOPER, RoleCode.PROGRAMME_MANAGER })]
        public async Task<IList<ThresholdViewModel>> GetAll()
            => await _service.GetAll();

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        [ValidateUserRole(RoleCode.ADMINISTRATOR)]
        public async Task<ThresholdViewModel> Update(ThresholdViewModel model)
            => await _service.Update(model);
    }
}
