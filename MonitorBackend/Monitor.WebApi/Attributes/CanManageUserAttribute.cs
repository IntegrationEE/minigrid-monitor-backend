using Microsoft.AspNetCore.Mvc;
using Monitor.WebApi.Filters;

namespace Monitor.WebApi
{
    /// <summary>
    /// Has access to User attribute
    /// </summary>
    public class CanManageUserAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Ctr
        /// </summary>
        public CanManageUserAttribute(string parameterName)
            : base(typeof(CanManageUserFilter))
        {
            Arguments = new object[] { parameterName };
        }
    }
}
