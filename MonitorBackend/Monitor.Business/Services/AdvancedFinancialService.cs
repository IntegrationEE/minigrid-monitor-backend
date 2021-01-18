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
    public class AdvancedFinancialService : IAdvancedFinancialService
    {
        private readonly IAdvancedAnalyticsRepository _repository;
        private readonly ISettingRepository _settingRepository;
        private readonly IChartConfigurationRepository _chartConfigurationRepository;

        public AdvancedFinancialService(IAdvancedAnalyticsRepository repository,
            ISettingRepository settingRepository,
            IChartConfigurationRepository chartConfigurationRepository)
        {
            _repository = repository;
            _settingRepository = settingRepository;
            _chartConfigurationRepository = chartConfigurationRepository;
        }

        public async Task<FinancialViewModel> GetCharts(FilterParametersViewModel filters)
        {
            var response = new FinancialViewModel();

            using (_repository)
            {
                var configs = await _chartConfigurationRepository.GetFinancial();

                await SetPricesPerConnection(filters, response);

                var config = configs.First(z => z.Code == ChartCode.CAPEX);
                response.CapexStructure = new BaseChartViewModel<string>(config.Map())
                {
                    TemporaryPoints = await GetCapexData(filters)
                };

                config = configs.First(z => z.Code == ChartCode.OPEX);
                response.OpexStructure = new BaseChartViewModel<string>(config.Map())
                {
                    TemporaryPoints = await GetOpexData(filters)
                };

                config = configs.First(z => z.Code == ChartCode.FINANCING_STRUCTURE);
                response.FinancingStructure = new BaseChartViewModel<string>(config.Map())
                {
                    TemporaryPoints = await GetFinanceData(filters)
                };

                config = configs.First(z => z.Code == ChartCode.REVENUE);
                var chartConfig = config.Map();
                response.Revenue = new ChartSeriesViewModel(chartConfig)
                {
                    Series = await GetRevenueSeries(filters, chartConfig),
                };

                response.ConvertCharts();
            }

            return response;
        }

        private async Task SetPricesPerConnection(FilterParametersViewModel filters, FinancialViewModel response)
        {
            var currency = await _settingRepository.GetCurrency();

            var capexConnections = await _repository.GetCapexPerConnections(filters);
            response.CapexPricePerConnection = $"{currency} {capexConnections}";

            var opexConnections = await _repository.GetOpexPerConnections(filters);
            response.OpexPricePerConnection = $"{currency} {opexConnections}";
        }

        private async Task<List<NamedChartViewModel<DateTime>>> GetRevenueSeries(FilterParametersViewModel filters, ChartConfig config)
        {
            var data = await _repository.GetRevenues(filters);

            var wrapper = new NamedChartGeneratorWrapper(config);
            wrapper.AddSeries(ChartConstans.COMMERCIAL, data.Select(x => (x.Year, Value: x.Commercial)));
            wrapper.AddSeries(ChartConstans.RESIDENTIAL, data.Select(x => (x.Year, Value: x.Residential)));
            wrapper.AddSeries(ChartConstans.PRODUCTIVE, data.Select(x => (x.Year, Value: x.Productive)));
            wrapper.AddSeries(ChartConstans.PUBLIC, data.Select(x => (x.Year, Value: x.Public)));

            if (data.Count() > ChartConstans.MIN_RANGE_FOR_TOTAL)
            {
                wrapper.AddSeries(ChartConstans.TOTAL, data.Select(x => (x.Year, Value: x.Total)));
            }

            return wrapper.GetSeries();
        }

        private async Task<List<(string key, decimal value)>> GetCapexData(FilterParametersViewModel filters)
        {
            var data = await _repository.GetCapex(filters);
            var points = new List<(string key, decimal value)>();

            points.AddPoint(ChartConstans.GENERATION, data.Generation, data.Total);
            points.AddPoint(ChartConstans.SITE_DEVELOPMENT, data.SiteDevelopment, data.Total);
            points.AddPoint(ChartConstans.LOGISTICS, data.Logistics, data.Total);
            points.AddPoint(ChartConstans.DISTRIBUTIONS, data.Distributions, data.Total);
            points.AddPoint(ChartConstans.COMMISSIONING, data.Commissioning, data.Total);
            points.AddPoint(ChartConstans.TAXES, data.Taxes, data.Total);
            points.AddPoint(ChartConstans.CUSTOMER_INSTALLATION, data.CustomerInstallation, data.Total);

            return points;
        }

        private async Task<List<(string key, decimal value)>> GetOpexData(FilterParametersViewModel filters)
        {
            var data = await _repository.GetOpex(filters);
            var points = new List<(string key, decimal value)>();

            points.AddPoint(ChartConstans.SITE_SPECIFIC, data.SiteSpecific, data.Total);
            points.AddPoint(ChartConstans.COMPANY_LEVEL, data.CompanyLevel, data.Total);
            points.AddPoint(ChartConstans.TAXES, data.Taxes, data.Total);
            points.AddPoint(ChartConstans.LOAN_REPAYMENTS, data.LoanRepayments, data.Total);

            return points;
        }

        public async Task<List<(string key, decimal value)>> GetFinanceData(FilterParametersViewModel filters)
        {
            var data = await _repository.GetFinance(filters);
            var points = new List<(string key, decimal value)>();

            points.AddPoint(ChartConstans.GRANT, data.Grant, data.Total);
            points.AddPoint(ChartConstans.EQUITY, data.Equity, data.Total);
            points.AddPoint(ChartConstans.DEBT, data.Debt, data.Total);

            return points;
        }
    }
}
