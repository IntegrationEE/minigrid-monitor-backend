using System.Linq;

namespace Monitor.Common.Models
{
    public class TargetedChartViewModel : BaseChartViewModel<int>
    {
        public TargetedChartViewModel(ChartConfig config)
            : base(config)
        { }

        public string Description { get; set; }

        public int Target { get; set; }

        public decimal? Value => Points.LastOrDefault()?.Value;

        public int? Key => Points.LastOrDefault()?.Key;
    }
}
