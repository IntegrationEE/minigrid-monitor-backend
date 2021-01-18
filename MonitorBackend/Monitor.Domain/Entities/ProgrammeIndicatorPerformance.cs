using Monitor.Domain.Base;

namespace Monitor.Domain.Entities
{
    public class ProgrammeIndicatorValue : BaseEntity, IBaseYearMonthIndicatorEntity
    {
        public ProgrammeIndicatorValue(int programmeIndicatorId, int year, int month)
        {
            ProgrammeIndicatorId = programmeIndicatorId;
            Year = year;
            Month = month;
        }

        public int Year { get; private set; }

        public int Month { get; private set; }

        public int ProgrammeIndicatorId { get; private set; }
        public virtual ProgrammeIndicator ProgrammeIndicator { get; private set; }

        public decimal? Value { get; private set; }

        public void Set(decimal? value)
        {
            Value = value;
        }
    }
}
