using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Domain.ViewModels;
using Monitor.Business.Services;
using Monitor.Domain.LightModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Users Controller
    /// </summary>
    public class UsersController : AuthorizeController
    {
        private readonly IUserService _service;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public UsersController(IUserService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ValidateUserRole(true, RoleCode.ADMINISTRATOR, RoleCode.DEVELOPER)]
        public async Task<IList<UserViewModel>> GetAll()
            => await _service.GetAll(CurrentUser);

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ValidateUserRole(true, RoleCode.ADMINISTRATOR, RoleCode.DEVELOPER, Order = 1)]
        [CanManageUser("id", Order = 2)]
        public async Task<UserViewModel> Get(int id)
            => await _service.Get(id);

        /// <summary>
        /// Get Current
        /// </summary>
        /// <returns></returns>
        [HttpGet("current")]
        public async Task<UserDetailsModel> GetCurrent()
            => await _service.GetCurrent(CurrentUserId);

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateUserRole(RoleCode.ADMINISTRATOR)]
        public async Task<UserViewModel> Post([FromBody] UserViewModel model)
            => await _service.Create(model);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [ValidateUserRole(RoleCode.ADMINISTRATOR)]
        public async Task<UserViewModel> Patch(int id, [FromBody] UserViewModel model)
            => await _service.Update(id, model);

        /// <summary>
        /// Update Current
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("current")]
        public async Task<UserDetailsModel> UpdateCurrent([FromBody] UserDetailsModel model)
            => await _service.UpdateCurrent(CurrentUserId, model);

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("password")]
        public async Task ChangePassword([FromBody] PasswordModel model)
            => await _service.ChangePassword(CurrentUserId, model);

        /// <summary>
        /// Approve
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}/approve")]
        [ValidateUserRole(true, RoleCode.DEVELOPER, RoleCode.ADMINISTRATOR, Order = 1)]
        [CanManageUser("id", Order = 2)]
        public async Task Approve(int id, [FromBody] ApproveModel model)
        {
            if (CurrentUser.Role == RoleCode.DEVELOPER)
            {
                model.Role = RoleCode.DEVELOPER;
                model.IsHeadOfCompany = false;
            }

            await _service.Approve(id, model);
        }

        /// <summary>
        /// Change Is Head of Company
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("{id}/headOfCompany")]
        [ValidateUserRole(true, RoleCode.DEVELOPER, Order = 1)]
        [CanManageUser("id", Order = 2)]
        public async Task ChangeHeadOfCompany(int id)
            => await _service.ToggleIsHeadOfCompany(id);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ValidateUserRole(true, RoleCode.ADMINISTRATOR, RoleCode.DEVELOPER, Order = 1)]
        [CanManageUser("id", Order = 2)]
        public async Task Delete(int id)
            => await _service.Delete(id);
    }
}