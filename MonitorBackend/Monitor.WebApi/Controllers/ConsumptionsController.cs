using Monitor.Common.Enums;
using Monitor.Domain.ViewModels;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Consumptions Controller
    /// </summary>
    [ValidateUserRole(new[] { RoleCode.DEVELOPER, RoleCode.PROGRAMME_MANAGER }, Order = 1)]
    [CanManageSite("parentId", Order = 2)]
    public class ConsumptionsController : BaseYearMonthIndicatorsController<ConsumptionViewModel, IConsumptionService>
    {
        /// <summary>
        /// Ctr
        /// </summary>
        public ConsumptionsController(IConsumptionService service)
            : base(service)
        { }
    }
}