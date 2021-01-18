using Monitor.Common.Logic;

namespace Monitor.Common.Models
{
    public class SocialViewModel
    {
        public ChartSeriesViewModel PeopleConnected { get; set; }

        public ChartSeriesViewModel EmploymentCreated { get; set; }

        public ChartSeriesViewModel NewServicesAvailable { get; set; }

        public BaseChartViewModel<string> CustomerSatisfaction { get; set; }

        public decimal Complaints { get; set; }

        public void ConvertCharts()
        {
            ConverterAdapter.GetConverter(PeopleConnected?.Convertable)?.Convert(PeopleConnected);
            ConverterAdapter.GetConverter(EmploymentCreated?.Convertable)?.Convert(EmploymentCreated);
            ConverterAdapter.GetConverter(NewServicesAvailable?.Convertable)?.Convert(NewServicesAvailable);
            ConverterAdapter.GetConverter(CustomerSatisfaction?.Convertable)?.Convert(CustomerSatisfaction);
        }
    }
}
