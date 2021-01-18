using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Common.Extensions;
using Monitor.Business.Extensions;
using Monitor.Business.Repositories;

namespace Monitor.Business.Services
{
    public class AdvancedSocialService : IAdvancedSocialService
    {
        private readonly IAdvancedAnalyticsRepository _repository;
        private readonly IChartConfigurationRepository _chartConfigurationRepository;

        public AdvancedSocialService(IAdvancedAnalyticsRepository repository, IChartConfigurationRepository chartConfigurationRepository)
        {
            _repository = repository;
            _chartConfigurationRepository = chartConfigurationRepository;
        }

        public async Task<SocialViewModel> GetCharts(FilterParametersViewModel filters)
        {
            var response = new SocialViewModel();

            using (_repository)
            {
                var configs = await _chartConfigurationRepository.GetSocial();

                var config = configs.First(z => z.Code == ChartCode.CONNECTIONS);
                var chartConfig = config.Map();
                response.PeopleConnected = new ChartSeriesViewModel(chartConfig)
                {
                    Series = await GetPeopleConnectedSeries(filters, chartConfig)
                };

                config = configs.First(z => z.Code == ChartCode.EMPLOYMENTS);
                chartConfig = config.Map();
                response.EmploymentCreated = new ChartSeriesViewModel(chartConfig)
                {
                    Series = await GetEmploymentSeries(filters, chartConfig)
                };

                config = configs.First(z => z.Code == ChartCode.NEW_SERVICES);
                chartConfig = config.Map();
                response.NewServicesAvailable = new ChartSeriesViewModel(chartConfig)
                {
                    Series = await GetNewServicesSeries(filters, chartConfig)
                };

                var (satisfactionPoints, complaints) = await GetSatisfactionData(filters);

                config = configs.First(z => z.Code == ChartCode.CUSTOMER_SATISFACTION);
                response.CustomerSatisfaction = new BaseChartViewModel<string>(config.Map())
                {
                    TemporaryPoints = satisfactionPoints
                };
                response.Complaints = complaints;

                response.ConvertCharts();
            }

            return response;
        }

        private async Task<List<NamedChartViewModel<DateTime>>> GetPeopleConnectedSeries(FilterParametersViewModel filters, ChartConfig config)
        {
            var data = await _repository.GetPeopleConnected(filters);

            var wrapper = new NamedChartGeneratorWrapper(config);
            wrapper.AddSeries(ChartConstans.COMMERCIAL, data.Select(x => (x.Year, Value: x.Commercial)));
            wrapper.AddSeries(ChartConstans.RESIDENTIAL, data.Select(x => (x.Year, Value: x.Residential)));
            wrapper.AddSeries(ChartConstans.PRODUCTIVE, data.Select(x => (x.Year, Value: x.Productive)));
            wrapper.AddSeries(ChartConstans.PUBLIC, data.Select(x => (x.Year, Value: x.Public)));

            return wrapper.GetSeries();
        }

        private async Task<List<NamedChartViewModel<DateTime>>> GetEmploymentSeries(FilterParametersViewModel filters, ChartConfig config)
        {
            var data = await _repository.GetEmployment(filters);

            var wrapper = new NamedChartGeneratorWrapper(config);
            wrapper.AddSeries(ChartConstans.DIRECT, data.Select(z => (z.Year, Value: z.Direct)));
            wrapper.AddSeries(ChartConstans.INDIRECT, data.Select(z => (z.Year, Value: z.Indirect)));

            return wrapper.GetSeries();
        }

        private async Task<List<NamedChartViewModel<DateTime>>> GetNewServicesSeries(FilterParametersViewModel filters, ChartConfig config)
        {
            var data = await _repository.GetNewServices(filters);

            var wrapper = new NamedChartGeneratorWrapper(config);
            wrapper.AddSeries(ChartConstans.COMMERCIAL, data.Select(x => (x.Year, Value: x.Commercial)));
            wrapper.AddSeries(ChartConstans.HEALTH, data.Select(x => (x.Year, Value: x.Health)));
            wrapper.AddSeries(ChartConstans.EDUCATION, data.Select(x => (x.Year, Value: x.Education)));
            wrapper.AddSeries(ChartConstans.PRODUCTIVE, data.Select(x => (x.Year, Value: x.Productive)));

            return wrapper.GetSeries();
        }

        public async Task<(List<(string key, decimal value)> satisfactionPoint, decimal complaints)> GetSatisfactionData(FilterParametersViewModel filters)
        {
            var data = await _repository.GetCustomerSatisfaction(filters);
            var points = new List<(string key, decimal value)>();

            points.AddPoint(ChartConstans.VERY_SATISFIED, data.VerySatisfied, data.Total);
            points.AddPoint(ChartConstans.SOMEHOW_SATISFIED, data.SomehowSatisfied, data.Total);
            points.AddPoint(ChartConstans.NEITHER_SATISFIED, data.NeitherSatisfiedNorUnsatisfied, data.Total);
            points.AddPoint(ChartConstans.SOMEHOW_UNSATISFIED, data.SomehowUnsatisfied, data.Total);
            points.AddPoint(ChartConstans.VERY_UNSATISFIED, data.VeryUnsatisfied, data.Total);

            return (points, data.Complaints);
        }
    }
}
