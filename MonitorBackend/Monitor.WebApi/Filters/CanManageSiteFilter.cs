using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Authorization;
using Monitor.Common;
using Monitor.Business.Services;
using Monitor.Common.Extensions;

namespace Monitor.WebApi.Filters
{
    /// <summary>
    /// Has access to Site Filter
    /// </summary>
    public class CanManageSiteFilter : IAsyncAuthorizationFilter
    {
        private readonly string _parameterName;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="parameterName"></param>
        public CanManageSiteFilter(string parameterName)
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
            var userId = context.HttpContext.User.Claims.GetUserId();
            var role = context.HttpContext.User.Claims.GetRole();

            if (context.HttpContext.Request.RouteValues.TryGetValue(_parameterName, out object id))
            {
                var siteId = Convert.ToInt32(id);

                if (!userId.HasValue || !await authService.CanManageSite(userId.Value, role, siteId))
                {
                    throw new CustomException($"You don't have access to Site width ID: '{siteId}'", HttpStatusCode.Forbidden);
                }
            }
        }
    }
}
