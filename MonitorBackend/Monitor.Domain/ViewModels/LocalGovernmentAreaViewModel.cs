using Monitor.Common;
using Monitor.Domain.Base;

namespace Monitor.Domain.ViewModels
{
    public class LocalGovernmentAreaViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }

        public void IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name))
            { throw new CustomException($"{nameof(Name)} is required."); }
            else if (StateId == 0)
            { throw new CustomException($"State is required."); }
        }
    }
}
