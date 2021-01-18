using Monitor.Common.Logic;
using System;

namespace Monitor.Common.Models
{
    public class TechnicalViewModel
    {
        public ChartSeriesViewModel InstalledCapacity { get; set; }

        public ChartSeriesViewModel AverageConsumption { get; set; }

        public ChartSeriesViewModel ElectricityConsumption { get; set; }

        public ChartSeriesViewModel DailyProfile { get; set; }

        public BaseChartViewModel<DateTime> CapacityUtilization { get; set; }

        public void ConvertCharts()
        {
            ConverterAdapter.GetConverter(InstalledCapacity?.Convertable)?.Convert(InstalledCapacity);
            ConverterAdapter.GetConverter(AverageConsumption?.Convertable)?.Convert(AverageConsumption);
            ConverterAdapter.GetConverter(ElectricityConsumption?.Convertable)?.Convert(ElectricityConsumption);
            ConverterAdapter.GetConverter(DailyProfile?.Convertable)?.Convert(DailyProfile);
            ConverterAdapter.GetConverter(CapacityUtilization?.Convertable)?.Convert(CapacityUtilization);
        }
    }
}

