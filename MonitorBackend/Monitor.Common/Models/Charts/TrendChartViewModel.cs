using System.Linq;
using Monitor.Common.Extensions;

namespace Monitor.Common.Models
{
    public class TrendChartViewModel : BaseChartViewModel<int>
    {
        public TrendChartViewModel(ChartConfig config)
            : base(config)
        { }

        public decimal? TrendValue
        {
            get
            {
                var points = Points.ToList();

                if (points.Count() < 2)
                { return null; }

                return points[^1].Value.ToPercentage(points[^2].Value, 0) - Constants.HUNDRED;
            }
        }

        public decimal? Value => Points.LastOrDefault()?.Value;

        public int? Key => Points.LastOrDefault()?.Key;
    }
}
