using Microsoft.AspNetCore.Mvc;
using Monitor.WebApi.Filters;

namespace Monitor.WebApi
{
    /// <summary>
    /// Has access to Programme's Indicator attribute
    /// </summary>
    public class CanManageProgrammeIndicatorAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Ctr
        /// </summary>
        public CanManageProgrammeIndicatorAttribute(string parameterName)
            : base(typeof(CanManageProgrammeIndicatorFilter))
        {
            Arguments = new object[] { parameterName };
        }
    }
}
