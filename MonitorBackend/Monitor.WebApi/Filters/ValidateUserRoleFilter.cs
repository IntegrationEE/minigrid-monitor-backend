using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Authorization;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Business.Services;
using Monitor.Common.Extensions;

namespace Monitor.WebApi.Filters
{
    /// <summary>
    /// Validate User Role Filter
    /// </summary>
    public class ValidateUserRoleFilter : IAsyncAuthorizationFilter
    {
        private readonly RoleCode[] _roles;
        private readonly bool _isHeadOfCompany;
        private string RequiredRoles => _roles?.Length > 0 ?
            string.Join(", ", _roles.Select(z => z.ToString())) :
            "Administrator";

        /// <summary>
        /// Constrcutor
        /// </summary>
        /// <param name="isHeadOfCompany"></param>
        /// <param name="roles"></param>
        public ValidateUserRoleFilter(bool isHeadOfCompany, RoleCode[] roles)
        {
            _roles = roles;
            _isHeadOfCompany = isHeadOfCompany;
        }
        /// <summary>
        /// User Roles Priviliges
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (actionDescriptor.FilterDescriptors.Any(z => z.Filter.GetType() == typeof(AllowAnonymousFilter)))
            { return; }

            var authService = context.HttpContext.RequestServices.GetService(typeof(IAuthenticationService)) as IAuthenticationService;
            var userId = context.HttpContext.User.Claims.GetUserId();
            var companyId = context.HttpContext.User.Claims.GetCompany();
            var role = context.HttpContext.User.Claims.GetRole();

            if (!userId.HasValue || !await authService.ValidateRole(userId.Value, _roles))
            {
                throw new CustomException($"You must be in role: '{RequiredRoles}' to do this.", HttpStatusCode.Forbidden);
            }
            else if (_isHeadOfCompany && role == RoleCode.DEVELOPER && (!companyId.HasValue || !await authService.IsHeadOfCompany(userId.Value, companyId.Value)))
            {
                throw new CustomException($"You must be Head of Company to do this.", HttpStatusCode.Forbidden);
            }
        }
    }
}
