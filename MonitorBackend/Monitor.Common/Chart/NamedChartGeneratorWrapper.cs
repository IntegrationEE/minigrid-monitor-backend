using System;
using System.Collections.Generic;
using Monitor.Common.Models;
using Monitor.Common.Extensions;

namespace Monitor.Common
{
    public class NamedChartGeneratorWrapper
    {
        private readonly ChartGenerator _generator;
        private readonly ChartConfig _config;
        private readonly List<NamedChartViewModel<DateTime>> _data;

        public NamedChartGeneratorWrapper(ChartConfig config)
        {
            _generator = new ChartGenerator();
            _config = config;
            _data = new List<NamedChartViewModel<DateTime>>();
        }

        public void AddSeries(string label, IEnumerable<(int Year, decimal Value)> data)
        {
            if (data.IsValidSeries())
            { _data.Add(_generator.GetNamedSeries(label, data, _config)); }
        }

        public void AddSeries(string label, IEnumerable<(int Year, int Month, decimal Value)> data)
        {
            if (data.IsValidSeries())
            { _data.Add(_generator.GetNamedSeriesWithMonth(label, data, _config)); }
        }

        public List<NamedChartViewModel<DateTime>> GetSeries()
            => _data;
    }
}
