using Monitor.Common.Logic;

namespace Monitor.Common.Models
{
    public class PortfolioViewModel
    {
        public TrendChartViewModel PeopleConnected { get; set; }

        public TrendChartViewModel CommunitiesConnected { get; set; }

        public TrendChartViewModel InstalledRenewableEnergyCapacity { get; set; }

        public TrendChartViewModel ElectricityConsumed { get; set; }

        public TrendChartViewModel TotalInvestment { get; set; }

        public TrendChartViewModel AverageTariff { get; set; }

        public void ConvertCharts()
        {
            ConverterAdapter.GetConverter(PeopleConnected.Convertable)?.Convert(PeopleConnected);
            ConverterAdapter.GetConverter(CommunitiesConnected.Convertable)?.Convert(CommunitiesConnected);
            ConverterAdapter.GetConverter(InstalledRenewableEnergyCapacity.Convertable)?.Convert(InstalledRenewableEnergyCapacity);
            ConverterAdapter.GetConverter(ElectricityConsumed.Convertable)?.Convert(ElectricityConsumed);
            ConverterAdapter.GetConverter(TotalInvestment.Convertable)?.Convert(TotalInvestment);
            ConverterAdapter.GetConverter(AverageTariff.Convertable)?.Convert(AverageTariff);
        }
    }
}
