using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Common.Enums;
using Monitor.Business.Services;
using Monitor.Domain.LightModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Integration records Controller
    /// </summary>
    [ValidateUserRole(RoleCode.ADMINISTRATOR)]
    public class IntegrationRecordsController : AuthorizeController
    {
        private readonly IIntegrationRecordService _service;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public IntegrationRecordsController(IIntegrationRecordService service)
        {
            _service = service;
        }
        /// <summary>
        /// Get All by Integration Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IList<IntegrationRecordLightModel>> GetAll(int id)
            => await _service.GetAll(id);
    }
}
