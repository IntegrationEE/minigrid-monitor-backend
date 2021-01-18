using Monitor.Common;
using Monitor.Domain.Base;
using Monitor.Common.Enums;
using Monitor.Common.Extensions;

namespace Monitor.Domain.ViewModels
{
    public class ThresholdViewModel : BaseViewModel
    {
        public ThresholdCode Code { get; set; }
        public string Name => Code.GetDescription();
        public decimal Min { get; set; }
        public decimal Max { get; set; }

        public void IsValid()
        {
            if (Min > Max)
            {
                throw new CustomException($"{nameof(Min)} has to be greater than {nameof(Max)}");
            }
        }
    }
}
