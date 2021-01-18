using Monitor.Common.Logic;

namespace Monitor.Common.Models
{
    public class FinancialViewModel
    {
        public BaseChartViewModel<string> FinancingStructure { get; set; }

        public BaseChartViewModel<string> OpexStructure { get; set; }

        public string OpexPricePerConnection { get; set; }

        public BaseChartViewModel<string> CapexStructure { get; set; }

        public string CapexPricePerConnection { get; set; }

        public ChartSeriesViewModel Revenue { get; set; }

        public void ConvertCharts()
        {
            ConverterAdapter.GetConverter(Revenue?.Convertable)?.Convert(Revenue);
        }
    }
}
