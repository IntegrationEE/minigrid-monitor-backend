using System;
using System.Linq;
using System.Collections.Generic;
using Monitor.Common.Models;

namespace Monitor.Common
{
    public class ChartGenerator
    {
        public List<(int key, decimal value)> GetSeries(IEnumerable<(int Year, decimal Value)> data, ChartConfig config)
        {
            return GetPoints(data, config.MinCount);
        }

        public NamedChartViewModel<DateTime> GetNamedSeriesWithMonth(string name, IEnumerable<(int Year, int Month, decimal Value)> values, ChartConfig config)
        {
            var data = values.Select(z => (DateTime.SpecifyKind(new DateTime(z.Year, z.Month, 1), DateTimeKind.Utc), z.Value));

            return new NamedChartViewModel<DateTime>(config)
            {
                Name = name,
                TemporaryPoints = GetPoints(data, config.MinCount),
            };
        }

        public NamedChartViewModel<DateTime> GetNamedSeries(string name, IEnumerable<(int Year, decimal Value)> values, ChartConfig config)
        {
            var data = values.Select(z => (DateTime.SpecifyKind(new DateTime(z.Year, 1, 1), DateTimeKind.Utc), z.Value));

            return new NamedChartViewModel<DateTime>(config)
            {
                Name = name,
                TemporaryPoints = GetPoints(data, config.MinCount),
            };
        }

        private List<(TKey key, decimal value)> GetPoints<TKey>(IEnumerable<(TKey Year, decimal Value)> values, int minCount)
            => values.Count() >= minCount ?
                values.ToList() :
                new List<(TKey key, decimal value)>();
    }
}
