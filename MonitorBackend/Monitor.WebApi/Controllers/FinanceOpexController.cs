using Monitor.Common.Enums;
using Monitor.Business.Services;
using Monitor.Domain.ViewModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Finance Opex Controller
    /// </summary>
    [ValidateUserRole(new[] { RoleCode.DEVELOPER, RoleCode.PROGRAMME_MANAGER }, Order = 1)]
    [CanManageSite("parentId", Order = 2)]
    public class FinanceOpexController : BaseYearMonthIndicatorsController<FinanceOpexViewModel, IFinanceOpexService>
    {
        /// <summary>
        /// Ctr
        /// </summary>
        public FinanceOpexController(IFinanceOpexService service)
            : base(service)
        { }
    }
}
