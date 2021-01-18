using Monitor.Domain.Base;

namespace Monitor.Domain.ViewModels
{
    public class TariffViewModel : BaseVisiteSiteViewModel
    {
        public decimal Residential { get; set; }

        public decimal Commercial { get; set; }

        public decimal Public { get; set; }

        public decimal Productive { get; set; }
    }
}
