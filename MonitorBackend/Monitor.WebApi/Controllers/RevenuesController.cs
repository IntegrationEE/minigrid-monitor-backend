using Monitor.Common.Enums;
using Monitor.Domain.ViewModels;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Revenues Controller
    /// </summary>
    [ValidateUserRole(new[] { RoleCode.DEVELOPER, RoleCode.PROGRAMME_MANAGER }, Order = 1)]
    [CanManageSite("parentId", Order = 2)]
    public class RevenuesController : BaseYearMonthIndicatorsController<RevenueViewModel, IRevenueService>
    {
        /// <summary>
        /// Ctr
        /// </summary>
        public RevenuesController(IRevenueService service)
            : base(service)
        { }
    }
}
