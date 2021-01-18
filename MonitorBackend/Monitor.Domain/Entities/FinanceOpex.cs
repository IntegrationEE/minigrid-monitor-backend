using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class FinanceOpex : BaseSiteEntity, IBaseYearMonthIndicatorEntity
    {
        public FinanceOpex(int siteId, int year, int month)
            : base(siteId)
        {
            Year = year;
            Month = month;
        }

        public int Year { get; private set; }

        public int Month { get; private set; }

        public decimal? SiteSpecific { get; private set; }

        public decimal? CompanyLevel { get; private set; }

        public decimal? Taxes { get; private set; }

        public decimal? LoanRepayments { get; private set; }

        public void Set(decimal? siteSpecific, decimal? companyLevel, decimal? taxes, decimal? loanRepayments)
        {
            SiteSpecific = siteSpecific;
            CompanyLevel = companyLevel;
            Taxes = taxes;
            LoanRepayments = loanRepayments;
        }
    }
}
