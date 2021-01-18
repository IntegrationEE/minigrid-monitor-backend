using Monitor.Domain.Base;
using Monitor.Common.Enums;

namespace Monitor.Domain.ViewModels
{
    public class SettingViewModel : IViewModel
    {
        public SettingCode Code { get; set; }
        public string Value { get; set; }
    }
}
