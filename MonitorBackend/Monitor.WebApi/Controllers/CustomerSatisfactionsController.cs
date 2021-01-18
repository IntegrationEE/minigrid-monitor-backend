using Monitor.Business.Services;
using Monitor.Domain.ViewModels;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Customer Satisfactions Controller
    /// </summary>
    public class CustomerSatisfactionsController : BaseVisitSiteManageController<CustomerSatisfactionViewModel, ICustomerSatisfactionService>
    {
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="service"></param>
        public CustomerSatisfactionsController(ICustomerSatisfactionService service)
            : base(service)
        { }
    }
}
