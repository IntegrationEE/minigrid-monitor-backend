using Monitor.Business.Services;
using Monitor.Domain.ViewModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// People Connected Controller
    /// </summary>
    public class PeopleConnectedController : BaseVisitSiteManageController<PeopleConnectedViewModel, IPeopleConnectedService>
    {
        /// <summary>
        /// Ctr
        /// </summary>
        public PeopleConnectedController(IPeopleConnectedService service)
            : base(service)
        { }
    }
}
