using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class Revenue : BaseSiteEntity, IBaseYearMonthIndicatorEntity
    {
        public Revenue(int siteId, int year, int month)
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

        public void Set(decimal? residential, decimal? commercial, decimal? productive, decimal? publicValue)
        {
            Residential = residential;
            Commercial = commercial;
            Productive = productive;
            Public = publicValue;
        }
    }
}
