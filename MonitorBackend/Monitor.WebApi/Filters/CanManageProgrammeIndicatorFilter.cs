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
    /// Has access to Programme's indicator Filter
    /// </summary>
    public class CanManageProgrammeIndicatorFilter : IAsyncAuthorizationFilter
    {
        private readonly string _parameterName;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="parameterName"></param>
        public CanManageProgrammeIndicatorFilter(string parameterName)
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

            if (context.HttpContext.Request.RouteValues.TryGetValue(_parameterName, out object id))
            {
                var programmeIndicatorId = Convert.ToInt32(id);

                if (!userId.HasValue || !await authService.CanManageProgrammeIndicator(userId.Value, programmeIndicatorId))
                {
                    throw new CustomException($"You don't have access to Programme's indicator width ID: '{programmeIndicatorId}'", HttpStatusCode.Forbidden);
                }
            }
        }
    }
}
