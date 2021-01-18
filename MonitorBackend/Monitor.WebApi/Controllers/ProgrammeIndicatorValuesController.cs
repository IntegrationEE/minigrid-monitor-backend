using Monitor.Common.Enums;
using Monitor.Domain.ViewModels;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Programme's indicator performance Controller
    /// </summary>
    [ValidateUserRole(RoleCode.PROGRAMME_MANAGER, Order = 1)]
    [CanManageProgrammeIndicator("parentId", Order = 2)]
    public class ProgrammeIndicatorValuesController : BaseYearMonthIndicatorsController<ProgrammeIndicatorValueViewModel, IProgrammeIndicatorValueService>
    {
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public ProgrammeIndicatorValuesController(IProgrammeIndicatorValueService service)
            : base(service)
        { }
    }
}
