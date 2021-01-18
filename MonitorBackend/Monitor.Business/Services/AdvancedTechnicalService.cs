using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Business.Extensions;
using Monitor.Business.Repositories;

namespace Monitor.Business.Services
{
    public class AdvancedTechnicalService : IAdvancedTechnicalService
    {
        private readonly IAdvancedAnalyticsRepository _repository;
        private readonly IChartConfigurationRepository _chartConfigurationRepository;

        public AdvancedTechnicalService(IAdvancedAnalyticsRepository repository, IChartConfigurationRepository chartConfigurationRepository)
        {
            _repository = repository;
            _chartConfigurationRepository = chartConfigurationRepository;
        }

        public async Task<TechnicalViewModel> GetCharts(FilterParametersViewModel filters)
        {
            var response = new TechnicalViewModel();

            using (_repository)
            {
                var configs = await _chartConfigurationRepository.GetTechnical();

                var config = configs.First(z => z.Code == ChartCode.AVERAGE_CONSUMPTIONS);
                var chartConfig = config.Map();
                response.AverageConsumption = new ChartSeriesViewModel(chartConfig)
                {
                    Series = await GetAverageConsumptionSeries(filters, chartConfig)
                };

                config = configs.First(z => z.Code == ChartCode.CAPACITY_UTILIZATION);
                chartConfig = config.Map();
                response.CapacityUtilization = new BaseChartViewModel<DateTime>(chartConfig)
                {
                    TemporaryPoints = await GetCapacityUtilizationPoints(filters)
                };

                config = configs.First(z => z.Code == ChartCode.ELECTRICITY_CONSUMPTION);
                chartConfig = config.Map();
                response.ElectricityConsumption = new ChartSeriesViewModel(chartConfig)
                {
                    Series = await GetEletricityConsumptionSeries(filters, chartConfig)
                };

                if (filters.Level.HasValue && filters.Level.Value == DataLevel.SITE)
                {
                    config = configs.First(z => z.Code == ChartCode.DAILY_PROFILE);
                    response.DailyProfile = new ChartSeriesViewModel(config.Map());
                }
                else
                {
                    config = configs.First(z => z.Code == ChartCode.INSTALLED_CAPACITY);
                    chartConfig = config.Map();
                    response.InstalledCapacity = new ChartSeriesViewModel(chartConfig)
                    {
                        Series = await GetInstalledCapacitySeries(filters, chartConfig)
                    };
                }

                response.ConvertCharts();
            }

            return response;
        }

        public async Task<List<NamedChartViewModel<DateTime>>> GetAverageConsumptionSeries(FilterParametersViewModel filters, ChartConfig config)
        {
            var data = await _repository.GetAverageConsumption(filters);

            var wrapper = new NamedChartGeneratorWrapper(config);
            wrapper.AddSeries(ChartConstans.COMMERCIAL, data.Select(x => (x.Year, x.Month, Value: x.Commercial)));
            wrapper.AddSeries(ChartConstans.RESIDENTIAL, data.Select(x => (x.Year, x.Month, Value: x.Residential)));
            wrapper.AddSeries(ChartConstans.PRODUCTIVE, data.Select(x => (x.Year, x.Month, Value: x.Productive)));
            wrapper.AddSeries(ChartConstans.PUBLIC, data.Select(x => (x.Year, x.Month, Value: x.Public)));

            if (data.Count() > ChartConstans.MIN_RANGE_FOR_TOTAL)
            {
                wrapper.AddSeries(ChartConstans.TOTAL, data.Select(x => (x.Year, x.Month, Value: x.Total)));
            }

            return wrapper.GetSeries();
        }

        public async Task<List<NamedChartViewModel<DateTime>>> GetEletricityConsumptionSeries(FilterParametersViewModel filters, ChartConfig config)
        {
            var data = await _repository.GetEletricityConsumption(filters);

            var wrapper = new NamedChartGeneratorWrapper(config);
            wrapper.AddSeries(ChartConstans.COMMERCIAL, data.Select(x => (x.Year, Value: x.Commercial)));
            wrapper.AddSeries(ChartConstans.RESIDENTIAL, data.Select(x => (x.Year, Value: x.Residential)));
            wrapper.AddSeries(ChartConstans.PRODUCTIVE, data.Select(x => (x.Year, Value: x.Productive)));
            wrapper.AddSeries(ChartConstans.PUBLIC, data.Select(x => (x.Year, Value: x.Public)));

            return wrapper.GetSeries();
        }

        public async Task<List<(DateTime key, decimal value)>> GetCapacityUtilizationPoints(FilterParametersViewModel filters)
        {
            var data = await _repository.GetCapacityUtilization(filters);

            return data.Select(z => (key: z.Date, z.Value)).ToList();
        }


        public async Task<List<NamedChartViewModel<DateTime>>> GetInstalledCapacitySeries(FilterParametersViewModel filters, ChartConfig config)
        {
            var data = await _repository.GetInstalledCapacity(filters);

            var wrapper = new NamedChartGeneratorWrapper(config);
            wrapper.AddSeries(ChartConstans.PV, data.Select(x => (x.Year, Value: x.PV)));
            wrapper.AddSeries(ChartConstans.HYDRO, data.Select(x => (x.Year, Value: x.Hydro)));
            wrapper.AddSeries(ChartConstans.BIOMASS, data.Select(x => (x.Year, Value: x.Biomass)));
            wrapper.AddSeries(ChartConstans.WIND, data.Select(x => (x.Year, Value: x.Wind)));
            wrapper.AddSeries(ChartConstans.CONVENTIONAL, data.Select(x => (x.Year, Value: x.Conventional)));

            return wrapper.GetSeries();
        }
    }
}
