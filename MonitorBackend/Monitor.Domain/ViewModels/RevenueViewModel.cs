using Monitor.Domain.Base;

namespace Monitor.Domain.ViewModels
{
    public class RevenueViewModel : BaseYearMonthIndicatorModel
    {
        public decimal? Residential { get; set; }

        public decimal? Commercial { get; set; }

        public decimal? Productive { get; set; }

        public decimal? Public { get; set; }

        public override bool IsApplicable()
            => Residential.HasValue ||
            Commercial.HasValue ||
            Productive.HasValue ||
            Public.HasValue;
    }
}
