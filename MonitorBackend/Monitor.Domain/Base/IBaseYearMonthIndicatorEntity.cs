namespace Monitor.Domain.Base
{
    public interface IBaseYearMonthIndicatorEntity : IBaseEntity
    {
        int Year { get; }

        int Month { get; }
    }
}
