using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Domain.Base;
using Monitor.Common.Enums;
using Monitor.Domain.ViewModels;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// LGA Controller
    /// </summary>
    public class LocalGovernmentAreasController : BaseCRUDController<LocalGovernmentAreaViewModel, ILocalGovernmentAreaService>
    {
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public LocalGovernmentAreasController(ILocalGovernmentAreaService service)
            : base(service)
        { }
        /// <summary>
        /// Get LGA by State
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        [HttpGet("{stateId}/states")]
        public async Task<IList<BaseLightModel>> GetByState(int stateId)
            => await Service.GetByState(stateId);
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateUserRole(RoleCode.ADMINISTRATOR)]
        public override Task<LocalGovernmentAreaViewModel> Post([FromBody] LocalGovernmentAreaViewModel model)
            => base.Post(model);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateUserRole(RoleCode.ADMINISTRATOR)]
        public override Task<LocalGovernmentAreaViewModel> Patch(int id, [FromBody] LocalGovernmentAreaViewModel model)
            => base.Patch(id, model);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ValidateUserRole(RoleCode.ADMINISTRATOR)]
        public override Task Delete(int id) =>
            base.Delete(id);
    }
}