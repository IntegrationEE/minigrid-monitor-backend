using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Monitor.Common.Extensions;

namespace Monitor.Common.Models
{
    public abstract class BaseChartPointViewModel<TKey>
    {
        private IEnumerable<PointViewModel<TKey, decimal>> _points;

        public BaseChartPointViewModel(bool isCumulative, int places)
        {
            Places = places;
            IsCumulative = isCumulative;
            TemporaryPoints = new List<(TKey Key, decimal Value)>();
        }

        [JsonIgnore]
        public List<(TKey Key, decimal Value)> TemporaryPoints { get; set; }

        [JsonIgnore]
        public int Places { get; set; }

        private bool IsCumulative { get; set; }

        public IEnumerable<PointViewModel<TKey, decimal>> Points
        {
            get { return _points ?? GetPoints(Places); }
            set { _points = value; }
        }

        public void SetPointsWithoutRounding()
        {
            _points = GetPoints(null);
        }

        private IEnumerable<PointViewModel<TKey, decimal>> GetPoints(int? places)
        {
            var points = TemporaryPoints.OrderBy(z => z.Key);

            if (IsCumulative)
            {
                return Cumulate(points, places);
            }

            return points.Select(z => new PointViewModel<TKey, decimal>()
            {
                Key = z.Key,
                Value = places.HasValue ? z.Value.Round(places.Value) : z.Value
            });
        }

        private IEnumerable<PointViewModel<TKey, decimal>> Cumulate(IEnumerable<(TKey key, decimal value)> items, int? places)
        {
            decimal sum = 0;

            foreach (var (key, value) in items)
            {
                sum += value;

                yield return new PointViewModel<TKey, decimal>
                {
                    Key = key,
                    Value = places.HasValue ? sum.Round(places.Value) : sum
                };
            }
        }
    }
}
