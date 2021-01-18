namespace Monitor.Business.Common
{
    public class OpexModel
    {
        public decimal SiteSpecific { get; set; }
        public decimal CompanyLevel { get; set; }
        public decimal Taxes { get; set; }
        public decimal LoanRepayments { get; set; }

        public decimal Total => SiteSpecific + CompanyLevel + Taxes + LoanRepayments;
    }
}
