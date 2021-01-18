using Microsoft.AspNetCore.Mvc;
using Monitor.WebApi.Filters;

namespace Monitor.WebApi
{
    /// <summary>
    /// Has access to Site attribute
    /// </summary>
    public class CanManageSiteAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Ctr
        /// </summary>
        public CanManageSiteAttribute(string parameterName)
            : base(typeof(CanManageSiteFilter))
        {
            Arguments = new object[] { parameterName };
        }
    }
}
