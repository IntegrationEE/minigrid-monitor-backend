using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Domain.Base;
using Monitor.Common.Enums;
using Monitor.Business.Services;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Companies Controller
    /// </summary>
    public class CompaniesController : BaseCRUDController<CompanyViewModel, ICompanyService>
    {
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public CompaniesController(ICompanyService service)
            : base(service)
        { }
        /// <summary>
        /// Get List
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IList<BaseLightModel>> GetList() =>
            await Service.GetList();
        /// <summary>
        /// Get List
        /// </summary>
        /// <returns></returns>
        [HttpGet("filters")]
        public async Task<IList<FilterLightModel>> GetListForFilters() =>
            await Service.GetListForFilters(CurrentUser);
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateUserRole(RoleCode.ADMINISTRATOR)]
        public override Task<CompanyViewModel> Post([FromBody] CompanyViewModel model)
            => base.Post(model);
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [ValidateUserRole(RoleCode.ADMINISTRATOR)]
        public override Task<CompanyViewModel> Patch(int id, [FromBody] CompanyViewModel model)
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
