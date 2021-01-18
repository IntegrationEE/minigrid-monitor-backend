using Monitor.Domain.ViewModels;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Tariffs Controller
    /// </summary>
    public class TariffsController : BaseVisitSiteManageController<TariffViewModel, ITariffService>
    {
        /// <summary>
        /// Ctr
        /// </summary>
        public TariffsController(ITariffService service)
            : base(service)
        { }
    }
}
