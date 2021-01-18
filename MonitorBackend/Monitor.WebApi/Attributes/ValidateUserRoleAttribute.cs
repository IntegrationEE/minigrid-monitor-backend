using Microsoft.AspNetCore.Mvc;
using Monitor.Common.Enums;
using Monitor.WebApi.Filters;

namespace Monitor.WebApi
{
    /// <summary>
    /// Validate Roles attribute
    /// </summary>
    public class ValidateUserRoleAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="roles"></param>
        public ValidateUserRoleAttribute(params RoleCode[] roles)
            : base(typeof(ValidateUserRoleFilter))
        {
            Arguments = new object[] { false, roles ?? new RoleCode[] { } };
        }
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="isHeadOfCompany"></param>
        /// <param name="roles"></param>
        public ValidateUserRoleAttribute(bool isHeadOfCompany, params RoleCode[] roles)
            : base(typeof(ValidateUserRoleFilter))
        {
            Arguments = new object[] { isHeadOfCompany, roles ?? new RoleCode[] { } };
        }
    }
}
