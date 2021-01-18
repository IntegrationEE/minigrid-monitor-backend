using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Common.Enums;
using Monitor.Domain.ViewModels;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Chart configurations Controller
    /// </summary>
    [ValidateUserRole(RoleCode.ADMINISTRATOR)]
    public class ChartConfigurationsController : AuthorizeController
    {
        private readonly IChartConfigurationService _service;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public ChartConfigurationsController(IChartConfigurationService service)
        {
            _service = service;
        }
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IList<ChartConfigurationViewModel>> GetAll()
            => await _service.GetAll();
        /// <summary>
        /// Update 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<ChartConfigurationViewModel> Update(int id, [FromBody] ChartConfigurationViewModel model)
            => await _service.Update(id, model);
    }
}
