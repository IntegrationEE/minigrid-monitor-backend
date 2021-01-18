using Monitor.Domain.Base;
using Monitor.Common.Models;

namespace Monitor.Domain.Quartiles
{
    public class FinanceOpexQuartile : IQuartile
    {
        public double? SiteSpecificQ1 { get; set; }
        public double? SiteSpecificQ3 { get; set; }
        public Quartile SiteSpecificMinMax => new Quartile(SiteSpecificQ1, SiteSpecificQ3);

        public double? CompanyLevelQ1 { get; set; }
        public double? CompanyLevelQ3 { get; set; }
        public Quartile CompanyLevelMinMax => new Quartile(CompanyLevelQ1, CompanyLevelQ3);

        public double? TaxesQ1 { get; set; }
        public double? TaxesQ3 { get; set; }
        public Quartile TaxesMinMax => new Quartile(TaxesQ1, TaxesQ3);

        public double? LoanRepaymentsQ1 { get; set; }
        public double? LoanRepaymentsQ3 { get; set; }
        public Quartile LoanRepaymentsMinMax => new Quartile(LoanRepaymentsQ1, LoanRepaymentsQ3);
    }
}
