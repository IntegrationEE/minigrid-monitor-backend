using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class FinanceCapex : BaseSiteEntity
    {
        public FinanceCapex(int siteId)
            : base(siteId)
        { }

        public decimal Generation { get; private set; }

        public decimal SiteDevelopment { get; private set; }

        public decimal Logistics { get; private set; }

        public decimal Distribution { get; private set; }

        public decimal CustomerInstallation { get; private set; }

        public decimal Commissioning { get; private set; }

        public decimal Taxes { get; private set; }

        public decimal FinancingGrant { get; private set; }

        public decimal FinancingEquity { get; private set; }

        public decimal FinancingDebt { get; private set; }

        public void Set(decimal generation, decimal siteDevelopment, decimal logistics, decimal distribution,
            decimal customerInstallation, decimal commissioning, decimal taxes)
        {
            Generation = generation;
            SiteDevelopment = siteDevelopment;
            Logistics = logistics;
            Distribution = distribution;
            CustomerInstallation = customerInstallation;
            Commissioning = commissioning;
            Taxes = taxes;
        }

        public void SetFinancing(decimal grant, decimal equity, decimal debt)
        {
            FinancingGrant = grant;
            FinancingEquity = equity;
            FinancingDebt = debt;
        }
    }
}
