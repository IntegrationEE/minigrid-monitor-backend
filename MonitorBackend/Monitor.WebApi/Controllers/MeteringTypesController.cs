using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Monitor.Common.Enums;
using Monitor.Domain.ViewModels;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Metering Types Controller
    /// </summary>
    public class MeteringTypesController : BaseCRUDController<MeteringTypeViewModel, IMeteringTypeService>
    {
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public MeteringTypesController(IMeteringTypeService service)
            : base(service)
        { }
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateUserRole(RoleCode.ADMINISTRATOR)]
        public override Task<MeteringTypeViewModel> Post([FromBody] MeteringTypeViewModel model)
            => base.Post(model);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateUserRole(RoleCode.ADMINISTRATOR)]
        public override Task<MeteringTypeViewModel> Patch(int id, [FromBody] MeteringTypeViewModel model)
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