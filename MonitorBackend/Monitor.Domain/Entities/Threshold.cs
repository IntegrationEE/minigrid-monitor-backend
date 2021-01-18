using System.ComponentModel.DataAnnotations;
using Monitor.Domain.Base;
using Monitor.Common.Enums;

namespace Monitor.Domain.Entities
{
    public class Threshold : BaseEntity
    {
        public Threshold(ThresholdCode code)
        {
            Code = code;
        }

        public Threshold(ThresholdCode code, decimal min, decimal max)
        {
            Code = code;
            Set(min, max);
        }

        public ThresholdCode Code { get; private set; }

        [RegularExpression(@"^\d{0,4}(.\d{1,4})?$", ErrorMessage = "Min is out of range")]
        public decimal Min { get; private set; }

        [RegularExpression(@"^\d{0,4}(.\d{1,4})?$", ErrorMessage = "Max is out of range")]
        public decimal Max { get; private set; }

        public void Set(decimal min, decimal max)
        {
            Min = min;
            Max = max;
        }
    }
}
