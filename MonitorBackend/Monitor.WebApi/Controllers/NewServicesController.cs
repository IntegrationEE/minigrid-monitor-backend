using Monitor.Business.Services;
using Monitor.Domain.ViewModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// New Services Controller
    /// </summary>
    public class NewServicesController : BaseVisitSiteManageController<NewServiceViewModel, INewServiceService>
    {
        /// <summary>
        /// Ctr
        /// </summary>
        public NewServicesController(INewServiceService service)
            : base(service)
        { }
    }
}
