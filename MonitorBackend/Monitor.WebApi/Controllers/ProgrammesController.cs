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
    /// Programmes Controller
    /// </summary>
    public class ProgrammesController : BaseCRUDController<ProgrammeViewModel, IProgrammeService>
    {
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public ProgrammesController(IProgrammeService service)
            : base(service)
        { }
        /// <summary>
        /// Get 
        /// </summary>
        /// <returns></returns>
        [HttpGet("current")]
        public async Task<IList<ProgrammeViewModel>> GetByCurrent()
            => await Service.GetByCurrent(CurrentUser);
        /// <summary>
        /// Get list
        /// </summary>
        /// <returns></returns>
        [HttpGet("filters")]
        public async Task<IList<FilterLightModel>> GetList()
            => await Service.GetListForFilters(CurrentUser);
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateUserRole(RoleCode.ADMINISTRATOR)]
        public override Task<ProgrammeViewModel> Post([FromBody] ProgrammeViewModel model)
            => base.Post(model);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateUserRole(RoleCode.ADMINISTRATOR)]
        public override Task<ProgrammeViewModel> Patch(int id, [FromBody] ProgrammeViewModel model)
            => base.Patch(id, model);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ValidateUserRole(RoleCode.ADMINISTRATOR)]
        public override Task Delete(int id)
            => base.Delete(id);
    }
}
