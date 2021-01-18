using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Common.Enums;
using Monitor.Domain.ViewModels;
using Monitor.Business.Services;
using Monitor.Domain.LightModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Programmes Indicators Controller
    /// </summary>
    [ValidateUserRole(RoleCode.PROGRAMME_MANAGER, Order = 1)]
    [CanManageProgrammeIndicator("id", Order = 2)]
    public class ProgrammeIndicatorsController : AuthorizeController
    {
        private readonly IProgrammeIndicatorService _service;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public ProgrammeIndicatorsController(IProgrammeIndicatorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get Programme's indicators
        /// </summary>
        /// <param name="programmeId"></param>
        /// <returns></returns>
        [HttpGet("{programmeId}/programmes")]
        public async Task<IList<ProgrammeIndicatorLightModel>> GetByProgramme(int programmeId)
            => await _service.GetByProgramme(programmeId);

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ProgrammeIndicatorViewModel> Get(int id)
            => await _service.Get(id);

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ProgrammeIndicatorViewModel> Create(ProgrammeIndicatorViewModel model)
            => await _service.Create(model);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<ProgrammeIndicatorViewModel> Update(int id, ProgrammeIndicatorViewModel model)
            => await _service.Update(id, model);

        /// <summary>
        /// Enable/Disable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}/toggle")]
        public async Task ToggleIsEnabled(int id)
            => await _service.ToggleIsEnabled(id);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task Delete(int id)
            => await _service.Delete(id);
    }
}
