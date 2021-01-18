using Monitor.Common;
using Monitor.Domain.Base;

namespace Monitor.Domain.ViewModels
{
    public class ConsumptionViewModel : BaseYearMonthIndicatorModel
    {
        public decimal? Residential { get; set; }

        public decimal? Commercial { get; set; }

        public decimal? Productive { get; set; }

        public decimal? Public { get; set; }

        public decimal? PeakLoad { get; set; }

        public decimal? Total { get; set; }

        public override void IsValid()
        {
            if ((Residential ?? 0) + (Commercial ?? 0) + (Productive ?? 0) + (Public ?? 0) + (PeakLoad ?? 0) > (Total ?? 0))
            { throw new CustomException($"Sum of {nameof(Residential)}, {nameof(Commercial)}, {nameof(Productive)}, {nameof(Public)} & {nameof(PeakLoad)} is greater than {nameof(Total)}."); }

            base.IsValid();
        }

        public override bool IsApplicable()
            => Residential.HasValue ||
            Commercial.HasValue ||
            Productive.HasValue ||
            Public.HasValue ||
            PeakLoad.HasValue ||
            Total.HasValue;
    }
}
