using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Monitor.Common.Enums;

namespace Monitor.Common.Models
{
    public class ChartSeriesViewModel
    {
        public ChartSeriesViewModel(ChartConfig config)
        {
            Title = config.Title;
            Tooltip = config.Tooltip;
            UnitOfMeasure = config.UnitOfMeasure;
            Convertable = config.Convertable;
            Series = new List<NamedChartViewModel<DateTime>>();
        }

        public string Title { get; private set; }
        public string Tooltip { get; private set; }
        public string UnitOfMeasure { get; private set; }

        [JsonIgnore]
        public ConvertableType? Convertable { get; set; }

        public double YaxisStep { get; set; }

        public bool ShouldSerializeYaxisStep() => YaxisStep != 0;

        public List<NamedChartViewModel<DateTime>> Series { get; set; }

        public void SetUnitOfMeasure(string unitOfMeasure)
        {
            UnitOfMeasure = unitOfMeasure;
        }

        public void SetPointsWithoutRounding()
        {
            Series.ForEach(z => z.SetPointsWithoutRounding());
        }
    }
}
