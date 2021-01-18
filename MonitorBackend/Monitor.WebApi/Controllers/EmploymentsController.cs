using Monitor.Business.Services;
using Monitor.Domain.ViewModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Employments Controller
    /// </summary>
    public class EmploymentsController : BaseVisitSiteManageController<EmploymentViewModel, IEmploymentService>
    {
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public EmploymentsController(IEmploymentService service)
            : base(service)
        { }
    }
}