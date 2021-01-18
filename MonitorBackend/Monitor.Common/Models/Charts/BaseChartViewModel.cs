using Newtonsoft.Json;
using Monitor.Common.Enums;

namespace Monitor.Common.Models
{
    public class BaseChartViewModel<TKey> : BaseChartPointViewModel<TKey>
    {
        public BaseChartViewModel(ChartConfig config)
            : base(config.IsCumulative, config.Places)
        {
            Title = config.Title;
            Tooltip = config.Tooltip;
            UnitOfMeasure = config.UnitOfMeasure;
            Convertable = config.Convertable;
        }

        public string Title { get; private set; }
        public string Tooltip { get; private set; }
        public string UnitOfMeasure { get; private set; }

        [JsonIgnore]
        public ConvertableType? Convertable { get; set; }

        public void SetUnitOfMeasure(string unitOfMeasure)
        {
            UnitOfMeasure = unitOfMeasure;
        }
    }
}
