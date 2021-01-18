using Monitor.Domain.Base;

namespace Monitor.Domain.ViewModels
{
    public class FinanceOpexViewModel : BaseYearMonthIndicatorModel
    {
        public decimal? SiteSpecific { get; set; }

        public decimal? CompanyLevel { get; set; }

        public decimal? Taxes { get; set; }

        public decimal? LoanRepayments { get; set; }

        public override bool IsApplicable()
            => SiteSpecific.HasValue ||
            CompanyLevel.HasValue ||
            Taxes.HasValue ||
            LoanRepayments.HasValue;
    }
}
