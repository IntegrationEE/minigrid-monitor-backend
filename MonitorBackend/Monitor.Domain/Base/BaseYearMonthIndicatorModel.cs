using Monitor.Common;

namespace Monitor.Domain.Base
{
    public abstract class BaseYearMonthIndicatorModel : BaseViewModel
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public virtual void IsValid()
        {
            if (Month < 1 || Month > 12)
            { throw new CustomException($"{nameof(Month)} is out of range."); }
        }

        public abstract bool IsApplicable();
    }
}
