using Monitor.Domain.Base;
using Monitor.Common.Extensions;

namespace Monitor.Domain.Entities
{
    public class Consumption : BaseSiteEntity, IBaseYearMonthIndicatorEntity
    {
        public Consumption(int siteId, int year, int month)
            : base(siteId)
        {
            Year = year;
            Month = month;
        }

        public int Year { get; private set; }

        public int Month { get; private set; }

        public decimal? Residential { get; private set; }

        public decimal? Commercial { get; private set; }

        public decimal? Productive { get; private set; }

        public decimal? Public { get; private set; }

        public decimal? PeakLoad { get; private set; }

        public decimal? Total { get; private set; }

        public void Set(decimal? residential, decimal? commercial, decimal? productive, decimal? peakload, decimal? publicValue, decimal? total)
        {
            Residential = residential.MultiplyByThousand();
            Commercial = commercial.MultiplyByThousand();
            Productive = productive.MultiplyByThousand();
            PeakLoad = peakload.MultiplyByThousand();
            Public = publicValue.MultiplyByThousand();
            Total = total.MultiplyByThousand();
        }
    }
}
