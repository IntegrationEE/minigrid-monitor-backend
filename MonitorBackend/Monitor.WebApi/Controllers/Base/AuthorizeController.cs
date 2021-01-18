using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Monitor.Common.Models;
using Monitor.Common.Extensions;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Authorize Controller
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public abstract class AuthorizeController : ControllerBase
    {
        /// <summary>
        /// Current User
        /// </summary>
        /// <returns></returns>
        protected UserInfo CurrentUser => User.Claims.GetUser();
        /// <summary>
        /// Current User Id
        /// </summary>
        /// <returns></returns>
        protected int CurrentUserId => User.Claims.GetUserId().Value;
        /// <summary>
        /// Current Company Id
        /// </summary>
        /// <returns></returns>
        protected int? CurrentCompanyId => User.Claims.GetCompany();
    }
}