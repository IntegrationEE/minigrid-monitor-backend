using Monitor.Domain.Base;

namespace Monitor.Domain.ViewModels
{
    public class ProgrammeIndicatorValueViewModel : BaseYearMonthIndicatorModel
    {
        public decimal? Value { get; set; }

        public override bool IsApplicable()
            => Value.HasValue;
    }
}
