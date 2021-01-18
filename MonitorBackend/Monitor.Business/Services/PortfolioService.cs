using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Business.Extensions;
using Monitor.Business.Repositories;

namespace Monitor.Business.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IBaseRepository _repository;
        private readonly IChartConfigurationRepository _chartConfigurationRepository;
        private readonly ChartGenerator _chartGenerator;

        public PortfolioService(IBaseRepository repository,
            IChartConfigurationRepository chartConfigurationRepository)
        {
            _repository = repository;
            _chartConfigurationRepository = chartConfigurationRepository;
            _chartGenerator = new ChartGenerator();
        }

        public async Task<PortfolioViewModel> GetData(FilterParametersViewModel filters)
        {
            PortfolioViewModel response;

            using (_repository)
            {
                var configs = await _chartConfigurationRepository.GetOverview();

                var tariffs = await GetTariffs(filters);
                var peopleConnected = await GetPeopleConnected(filters);
                var consumptions = await GetConsumptions(filters);
                var capacities = await GetCapacities(filters);
                var communitiesConnected = await GetCommunitiesConnected(filters);
                var totalInvestments = await GetTotalInvestments(filters);

                response = new PortfolioViewModel
                {
                    AverageTariff = GetChart(tariffs, configs.First(z => z.Code == ChartCode.AVERAGE_TARIFF)),
                    PeopleConnected = GetChart(peopleConnected, configs.First(z => z.Code == ChartCode.PEOPLE_CONNECTED)),
                    ElectricityConsumed = GetChart(consumptions, configs.First(z => z.Code == ChartCode.ELECTRICITY_CONSUMED)),
                    InstalledRenewableEnergyCapacity = GetChart(capacities, configs.First(z => z.Code == ChartCode.INSTALLED_RENEWABLE)),
                    TotalInvestment = GetChart(totalInvestments, configs.First(z => z.Code == ChartCode.INVESTMENTS)),
                    CommunitiesConnected = GetChart(communitiesConnected, configs.First(z => z.Code == ChartCode.COMMUNITIES_CONNECTED))
                };

                response.ConvertCharts();
            }

            return response;
        }

        private async Task<IEnumerable<(int Year, decimal Value)>> GetTariffs(FilterParametersViewModel filters)
        {
            var data = await _repository.GetQuery<Tariff>()
                .Filter(filters)
                .Where(z => z.Residential > 0)
                .Select(x => new
                {
                    x.Residential,
                    x.VisitDate.Year,
                    x.SiteId
                })
                .GroupBy(x => new { x.Year, x.SiteId })
                .Select(x => new Tuple<int, int, decimal>(
                    x.Key.Year,
                    x.Key.SiteId,
                    x.Average(z => z.Residential))
                ).ToListAsync();

            return data
                .GroupBy(z => z.Item1)
                .Select(x => (Year: x.Key, Value: x.Sum(z => z.Item3) / x.Count()));
        }

        public async Task<IEnumerable<(int Year, decimal Value)>> GetPeopleConnected(FilterParametersViewModel filters)
        {
            var peopleInHousehold = await _repository.GetQuery<Setting>(z => z.Code == SettingCode.PEOPLE_IN_HOUSEHOLD)
                .FirstAsync();

            var data = await _repository.GetQuery<PeopleConnected>()
                .Filter(filters)
                .Select(x => new
                {
                    Total = x.Residential + x.Commercial + x.Productive + x.Public,
                    x.VisitDate.Year
                })
                .GroupBy(x => x.Year)
                .Select(x => new Tuple<int, decimal>(x.Key, x.Sum(z => z.Total * peopleInHousehold.GetIntValue())))
                .ToListAsync();

            return data.Select(x => (Year: x.Item1, Value: x.Item2));
        }

        private async Task<IEnumerable<(int Year, decimal Value)>> GetCommunitiesConnected(FilterParametersViewModel filters)
        {
            var data = await _repository.GetQuery<Site>()
                .Filter(filters)
                .GroupBy(x => x.CommissioningDate.Year)
                .Select(x => new Tuple<int, decimal>(x.Key, x.Count()))
                .ToListAsync();

            return data.Select(x => (Year: x.Item1, Value: x.Item2));
        }

        private async Task<IEnumerable<(int Year, decimal Value)>> GetCapacities(FilterParametersViewModel filters)
        {
            var data = await _repository.GetQuery<SiteTechParameter>()
                .Filter(filters)
                .Select(x => new { x.Site.CommissioningDate.Year, x.RenewableCapacity })
                .GroupBy(x => x.Year)
                .Select(x => new Tuple<int, decimal>(x.Key, x.Sum(z => z.RenewableCapacity)))
                .ToListAsync();

            return data.Select(x => (Year: x.Item1, Value: x.Item2));
        }

        private async Task<IEnumerable<(int Year, decimal Value)>> GetConsumptions(FilterParametersViewModel filters)
        {
            var data = await _repository.GetQuery<Consumption>()
                .Filter(filters)
                .Select(x => new { x.Year, Total = (x.Commercial ?? 0) + (x.Productive ?? 0) + (x.Public ?? 0) + (x.Residential ?? 0) })
                .GroupBy(x => x.Year)
                .Select(x => new Tuple<int, decimal>(x.Key, x.Sum(z => z.Total)))
                .ToListAsync();

            return data.Select(x => (Year: x.Item1, Value: x.Item2));
        }

        private async Task<IEnumerable<(int Year, decimal Value)>> GetTotalInvestments(FilterParametersViewModel filters)
        {
            var data = await _repository.GetQuery<FinanceCapex>()
                .Filter(filters)
                .Select(z => new
                {
                    z.Site.CommissioningDate.Year,
                    Total = z.Commissioning + z.CustomerInstallation + z.Distribution + z.Logistics + z.Taxes + z.SiteDevelopment + z.Generation
                })
                .GroupBy(x => x.Year)
                .Select(x => new Tuple<int, decimal>(x.Key, x.Sum(z => z.Total)))
                .ToListAsync();

            return data.Select(x => (Year: x.Item1, Value: x.Item2));
        }

        private TrendChartViewModel GetChart(IEnumerable<(int Year, decimal Value)> points, ChartConfiguration config)
        {
            const int DECIMAL_PLACES = 0;

            var chartConfig = config.Map(DECIMAL_PLACES);
            var model = new TrendChartViewModel(chartConfig)
            {
                TemporaryPoints = _chartGenerator.GetSeries(points, chartConfig)
            };

            return model;
        }
    }
}
