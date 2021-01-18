using Monitor.Common;
using Monitor.Domain.Base;

namespace Monitor.Domain.ViewModels
{
    public class ProgrammeIndicatorViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public int Target { get; set; }
        public bool IsCumulative { get; set; }
        public int ProgrammeId { get; set; }

        public void IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name))
            { throw new CustomException($"{nameof(Name)} is required."); }
        }
    }
}
