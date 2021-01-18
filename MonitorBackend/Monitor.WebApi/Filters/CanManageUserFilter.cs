using System;
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
    /// Has access to User Filter
    /// </summary>
    public class CanManageUserFilter : IAsyncAuthorizationFilter
    {
        private readonly string _parameterName;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="parameterName"></param>
        public CanManageUserFilter(string parameterName)
        {
            _parameterName = parameterName;
        }
        /// <summary>
        /// Validate
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (actionDescriptor.FilterDescriptors.Any(z => z.Filter.GetType() == typeof(AllowAnonymousFilter)))
            { return; }

            var authService = context.HttpContext.RequestServices.GetService(typeof(IAuthenticationService)) as IAuthenticationService;      
            var role = context.HttpContext.User.Claims.GetRole();

            if (role != RoleCode.ADMINISTRATOR && context.HttpContext.Request.RouteValues.TryGetValue(_parameterName, out object id))
            {
                var companyId = context.HttpContext.User.Claims.GetCompany();
                var userId = Convert.ToInt32(id);

                if (!companyId.HasValue || !await authService.CanManageUser(companyId.Value, userId))
                {
                    throw new CustomException($"You don't have access to User width ID: '{userId}'", HttpStatusCode.Forbidden);
                }
            }
        }
    }
}
