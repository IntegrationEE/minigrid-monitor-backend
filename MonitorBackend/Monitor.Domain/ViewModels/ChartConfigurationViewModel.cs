using Newtonsoft.Json;
using Monitor.Common;
using Monitor.Domain.Base;
using Monitor.Common.Enums;
using Monitor.Common.Extensions;

namespace Monitor.Domain.ViewModels
{
    public class ChartConfigurationViewModel : BaseViewModel
    {
        public ChartCode Code { get; set; }
        [JsonIgnore]
        public ChartType Type { get; set; }

        public string Title { get; set; }
        public string Tooltip { get; set; }
        public string UnitOfMeasure { get; set; }
        public bool IsCumulative { get; set; }
        public ConvertableType? Convertable { get; set; }

        public string Name => Code.GetDescription();
        public string TypeName => Type.GetDescription();

        public void IsValid()
        {
            if (string.IsNullOrWhiteSpace(Title))
            { throw new CustomException($"{nameof(Title)} is required."); }
        }
    }
}
