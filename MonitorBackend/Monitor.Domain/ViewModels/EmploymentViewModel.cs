using Monitor.Domain.Base;

namespace Monitor.Domain.ViewModels
{
    public class EmploymentViewModel : BaseVisiteSiteViewModel
    {
        public int Indirect { get; set; }

        public int Direct { get; set; }
    }
}
