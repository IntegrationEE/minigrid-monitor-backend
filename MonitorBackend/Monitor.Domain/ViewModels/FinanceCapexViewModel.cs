using Monitor.Domain.Base;

namespace Monitor.Domain.ViewModels
{
    public class FinanceCapexViewModel : BaseViewModel
    {
        public int SiteId { get; set; }

        public decimal Generation { get; set; }

        public decimal SiteDevelopment { get; set; }

        public decimal Logistics { get; set; }

        public decimal Distribution { get; set; }

        public decimal CustomerInstallation { get; set; }

        public decimal Commissioning { get; set; }

        public decimal Taxes { get; set; }

        public decimal FinancingGrant { get; set; }

        public decimal FinancingEquity { get; set; }

        public decimal FinancingDebt { get; set; }
    }
}
