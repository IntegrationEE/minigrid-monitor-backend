using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Business.Common;
using Monitor.Common.Extensions;
using Monitor.Business.Extensions;

namespace Monitor.Business.Repositories
{
    public class AdvancedAnalyticsRepository : IAdvancedAnalyticsRepository
    {
        private readonly IBaseRepository _repository;

        public AdvancedAnalyticsRepository(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PeopleConnectedModel>> GetPeopleConnected(FilterParametersViewModel filters)
        {
            return await _repository.GetQuery<PeopleConnected>()
                .Filter(filters)
                .Where(z => z.Commercial > 0 || z.Productive > 0 || z.Residential > 0 || z.Public > 0)
                .Select(x => new
                {
                    x.Commercial,
                    x.Productive,
                    x.Residential,
                    x.Public,
                    x.VisitDate.Year
                })
                .GroupBy(x => x.Year)
                .Select(x => new PeopleConnectedModel
                {
                    Year = x.Key,
                    Commercial = x.Sum(z => z.Commercial),
                    Productive = x.Sum(z => z.Productive),
                    Residential = x.Sum(z => z.Residential),
                    Public = x.Sum(z => z.Public)
                })
                .OrderBy(z => z.Year)
                .ToListAsync();
        }

        public async Task<IEnumerable<EmploymentModel>> GetEmployment(FilterParametersViewModel filters)
        {
            return await _repository.GetQuery<Employment>()
                .Filter(filters)
                .Where(z => z.Indirect > 0 || z.Direct > 0)
                .Select(x => new
                {
                    x.Indirect,
                    x.Direct,
                    x.VisitDate.Year
                })
                .GroupBy(x => x.Year)
                .Select(x => new EmploymentModel
                {
                    Year = x.Key,
                    Indirect = x.Sum(z => z.Indirect),
                    Direct = x.Sum(z => z.Direct)
                })
                .OrderBy(z => z.Year)
                .ToListAsync();
        }

        public async Task<IEnumerable<ServicesModel>> GetNewServices(FilterParametersViewModel filters)
        {
            return await _repository.GetQuery<NewService>()
                .Filter(filters)
                .Where(z => z.Commercial > 0 || z.Health > 0 || z.Education > 0 || z.Productive > 0)
                .Select(x => new
                {
                    x.Commercial,
                    x.Education,
                    x.Health,
                    x.Productive,
                    x.VisitDate.Year
                })
                .GroupBy(x => x.Year)
                .Select(x => new ServicesModel
                {
                    Year = x.Key,
                    Commercial = x.Sum(z => z.Commercial),
                    Education = x.Sum(z => z.Education),
                    Health = x.Sum(z => z.Health),
                    Productive = x.Sum(z => z.Productive)
                })
                .OrderBy(z => z.Year)
                .ToListAsync();
        }

        public async Task<CustomerSatisfactionModel> GetCustomerSatisfaction(FilterParametersViewModel filters)
        {
            var data = await _repository.GetQuery<CustomerSatisfaction>()
                .Filter(filters)
                .Where(z => z.Satisfaction > 0)
                .Select(x => new { x.Type, x.Satisfaction })
                .GroupBy(x => x.Type)
                .Select(x => new Tuple<SatisfactionType, decimal>(x.Key, x.Sum(z => z.Satisfaction)))
                .ToListAsync();

            return new CustomerSatisfactionModel
            {
                VerySatisfied = data.Where(z => z.Item1 == SatisfactionType.VERY_SATISFIED).Sum(x => x.Item2),
                SomehowSatisfied = data.Where(z => z.Item1 == SatisfactionType.SOMEHOW_SATISFIED).Sum(x => x.Item2),
                NeitherSatisfiedNorUnsatisfied = data.Where(z => z.Item1 == SatisfactionType.NEITHER_SATISFIED_NOR_UNSATISFIED).Sum(x => x.Item2),
                SomehowUnsatisfied = data.Where(z => z.Item1 == SatisfactionType.SOMEHOW_UNSATISFIED).Sum(x => x.Item2),
                VeryUnsatisfied = data.Where(z => z.Item1 == SatisfactionType.VERY_UNSATISFIED).Sum(x => x.Item2)
            };
        }

        public async Task<IEnumerable<RevenueModel>> GetRevenues(FilterParametersViewModel filters)
        {
            return await _repository.GetQuery<Revenue>()
                .Filter(filters)
                .Where(z => (z.Productive ?? 0) > 0 || (z.Commercial ?? 0) > 0 || (z.Residential ?? 0) > 0 || (z.Public ?? 0) > 0)
                .Select(x => new
                {
                    Productive = x.Productive ?? 0,
                    Residential = x.Residential ?? 0,
                    Commercial = x.Commercial ?? 0,
                    Public = x.Public ?? 0,
                    x.Year
                })
                .GroupBy(x => x.Year)
                .Select(x => new RevenueModel
                {
                    Year = x.Key,
                    Commercial = x.Sum(z => z.Commercial),
                    Productive = x.Sum(z => z.Productive),
                    Residential = x.Sum(z => z.Residential),
                    Public = x.Sum(z => z.Public)
                })
                .OrderBy(z => z.Year)
                .ToListAsync();
        }

        public async Task<CapexModel> GetCapex(FilterParametersViewModel filters)
        {
            var data = await _repository.GetQuery<FinanceCapex>()
                .Filter(filters)
                .Where(z => z.Distribution > 0 || z.SiteDevelopment > 0 || z.Commissioning > 0 || z.Generation > 0 || z.Logistics > 0 || z.Taxes > 0)
                .GroupBy(z => true)
                .Select(x => new CapexModel
                {
                    Distributions = x.Sum(z => z.Distribution),
                    SiteDevelopment = x.Sum(z => z.SiteDevelopment),
                    Commissioning = x.Sum(z => z.Commissioning),
                    Generation = x.Sum(z => z.Generation),
                    Logistics = x.Sum(z => z.Logistics),
                    Taxes = x.Sum(z => z.Taxes),
                }).FirstOrDefaultAsync();

            return data ?? new CapexModel();
        }

        public async Task<OpexModel> GetOpex(FilterParametersViewModel filters)
        {
            var data = await _repository.GetQuery<FinanceOpex>()
                .Filter(filters)
                .Where(z => (z.SiteSpecific ?? 0) > 0 || (z.CompanyLevel ?? 0) > 0 || (z.LoanRepayments ?? 0) > 0 || (z.Taxes ?? 0) > 0)
                .GroupBy(z => true)
                .Select(x => new OpexModel
                {
                    SiteSpecific = x.Sum(z => z.SiteSpecific ?? 0),
                    CompanyLevel = x.Sum(z => z.CompanyLevel ?? 0),
                    LoanRepayments = x.Sum(z => z.LoanRepayments ?? 0),
                    Taxes = x.Sum(z => z.Taxes ?? 0),
                }).FirstOrDefaultAsync();

            return data ?? new OpexModel();
        }

        public async Task<FinanceModel> GetFinance(FilterParametersViewModel filters)
        {
            var data = await _repository.GetQuery<FinanceCapex>()
                .Filter(filters)
                .Where(z => z.FinancingDebt > 0 || z.FinancingEquity > 0 || z.FinancingGrant > 0)
                .GroupBy(z => true)
                .Select(x => new FinanceModel
                {
                    Debt = x.Sum(z => z.FinancingDebt),
                    Equity = x.Sum(z => z.FinancingEquity),
                    Grant = x.Sum(z => z.FinancingGrant),
                }).FirstOrDefaultAsync();

            return data ?? new FinanceModel();
        }

        public async Task<decimal> GetCapexPerConnections(FilterParametersViewModel filters)
        {
            var items = await _repository.GetQuery<FinanceCapex>()
                .Filter(filters)
                .Where(z => z.Site.PeopleConnected.Any())
                .GroupBy(z => z.SiteId)
                .Select(z => new
                {
                    SiteId = z.Key,
                    Total = z.Sum(x =>
                        x.Taxes + x.Generation + x.SiteDevelopment + x.Logistics + x.Distribution + x.Commissioning +
                        x.CustomerInstallation + x.FinancingDebt + x.FinancingEquity + x.FinancingGrant
                    )
                }).ToListAsync();

            var totalConnections = await GetTotalConnections(filters, items.Select(z => z.SiteId));

            return totalConnections > 0 && items.Count > 0 ?
                (items.Sum(z => z.Total) / (totalConnections * items.Count)).Round(0) :
                0;
        }

        public async Task<decimal> GetOpexPerConnections(FilterParametersViewModel filters)
        {
            var items = await _repository.GetQuery<FinanceOpex>()
                .Filter(filters)
                .Where(z => z.Site.PeopleConnected.Any())
                .GroupBy(z => z.SiteId)
                .Select(z => new
                {
                    SiteId = z.Key,
                    Total = z.Sum(x => (x.SiteSpecific ?? 0) + (x.CompanyLevel ?? 0) + (x.Taxes ?? 0) + (x.LoanRepayments ?? 0))
                })
                .Where(z => z.Total > 0)
                .ToListAsync();

            var totalConnections = await GetTotalConnections(filters, items.Select(z => z.SiteId));

            return totalConnections > 0 && items.Count > 0 ?
                (items.Sum(z => z.Total) / (totalConnections * items.Count)).Round(0) :
                0;
        }

        public async Task<IEnumerable<AverageConsumptionModel>> GetAverageConsumption(FilterParametersViewModel filters)
        {
            var currentYear = DateTime.Now.Year;

            return await _repository.GetQuery<Consumption>()
                .Filter(filters)
                .Select(x => new
                {
                    Productive = x.Productive ?? 0,
                    Residential = x.Residential ?? 0,
                    Commercial = x.Commercial ?? 0,
                    Public = x.Public ?? 0,
                    x.Year,
                    x.Month
                })
                .GroupBy(x => new { x.Month })
                .Select(x => new AverageConsumptionModel
                {
                    Year = currentYear,
                    Month = x.Key.Month,
                    Productive = x.Average(z => z.Productive),
                    Residential = x.Average(z => z.Residential),
                    Commercial = x.Average(z => z.Commercial),
                    Public = x.Average(z => z.Public)
                })
                .OrderBy(z => z.Year).ThenBy(z => z.Month)
                .ToListAsync();
        }

        public async Task<List<CapacityUtilizationModel>> GetCapacityUtilization(FilterParametersViewModel filters)
        {
            var consumptions = await _repository.GetQuery<Consumption>()
                .Filter(filters)
                .Where(z => (z.PeakLoad ?? 0) > 0)
                .Select(x => new
                {
                    PeakLoad = x.PeakLoad.Value,
                    x.Year,
                    x.Month
                })
                .GroupBy(x => new { x.Year, x.Month })
                .Select(x => new Tuple<int, int, decimal>(x.Key.Year, x.Key.Month, x.Sum(z => z.PeakLoad)))
                .ToListAsync();

            var capacities = await _repository.GetQuery<SiteTechParameter>()
                .Filter(filters)
                .Select(x => new
                {
                    x.Site.CommissioningDate.Year,
                    x.RenewableCapacity
                })
                .GroupBy(x => x.Year)
                .Select(x => new Tuple<int, decimal>(x.Key, x.Sum(z => z.RenewableCapacity)))
                .ToListAsync();

            var response = new List<CapacityUtilizationModel>();

            var years = capacities.Select(x => x.Item1)
                .Concat(consumptions.Select(z => z.Item1))
                .OrderBy(x => x)
                .Distinct();

            foreach (var year in years)
            {
                var capacity = capacities.Where(x => x.Item1 <= year).Sum(m => m.Item2);

                for (var month = 1; month <= 12; month++)
                {
                    var currentDate = new DateTime(year, month, 1);

                    var consumption = consumptions.Where(x => x.Item1 == year && x.Item2 == month)
                        .Select(m => m.Item3)
                        .FirstOrDefault();

                    if (consumption > 0 && consumption <= capacity)
                    {
                        response.Add(new CapacityUtilizationModel
                        {
                            Date = DateTime.SpecifyKind(currentDate, DateTimeKind.Utc),
                            Value = consumption.ToPercentage(capacity)
                        });
                    }
                }
            }

            return response;
        }

        public async Task<IEnumerable<ElectricityConsumptionModel>> GetEletricityConsumption(FilterParametersViewModel filters)
        {
            return await _repository.GetQuery<Consumption>()
                .Filter(filters)
                .Where(z => (z.Productive ?? 0) > 0 || (z.Residential ?? 0) > 0 || (z.Commercial ?? 0) > 0 || (z.Public ?? 0) > 0)
                .Select(x => new
                {
                    Productive = x.Productive ?? 0,
                    Residential = x.Residential ?? 0,
                    Commercial = x.Commercial ?? 0,
                    Public = x.Public ?? 0,
                    x.Year
                })
                .GroupBy(x => x.Year)
                .Select(x => new ElectricityConsumptionModel
                {
                    Year = x.Key,
                    Productive = x.Average(z => z.Productive),
                    Commercial = x.Average(z => z.Commercial),
                    Residential = x.Average(z => z.Residential),
                    Public = x.Average(z => z.Public)
                })
                .OrderBy(z => z.Year)
                .ToListAsync();
        }

        public async Task<List<InstalledCapacityModel>> GetInstalledCapacity(FilterParametersViewModel filters)
        {
            var data = await _repository.GetQuery<SiteTechParameter>()
                .Filter(filters)
                .Select(x => new
                {
                    x.RenewableTechnology,
                    x.RenewableCapacity,
                    x.Site.CommissioningDate.Year,
                    x.ConventionalCapacity
                })
                .GroupBy(x => new { x.Year, x.RenewableTechnology })
                .Select(x => new Tuple<int, RenewableTechnology, decimal, decimal>(
                    x.Key.Year,
                    x.Key.RenewableTechnology,
                    x.Sum(z => z.RenewableCapacity),
                    x.Sum(z => z.ConventionalCapacity)
                )).ToListAsync();

            var response = new List<InstalledCapacityModel>();

            var range = data.GroupBy(z => z.Item1)
                .Select(z => z.Key)
                .ToList();

            foreach (var year in range)
            {
                response.Add(new InstalledCapacityModel
                {
                    Year = year,
                    PV = data.Where(z => z.Item2 == RenewableTechnology.PV && z.Item1 == year).Sum(z => z.Item3),
                    Hydro = data.Where(z => z.Item2 == RenewableTechnology.HYDRO && z.Item1 == year).Sum(z => z.Item3),
                    Biomass = data.Where(z => z.Item2 == RenewableTechnology.BIOMASS && z.Item1 == year).Sum(z => z.Item3),
                    Wind = data.Where(z => z.Item2 == RenewableTechnology.WIND && z.Item1 == year).Sum(z => z.Item3),
                    Conventional = data.Where(z => z.Item1 == year).Sum(z => z.Item4)
                });
            }

            return response;
        }

        private async Task<int> GetTotalConnections(FilterParametersViewModel filters, IEnumerable<int> siteIds)
        {
            var data = await _repository.GetQuery<PeopleConnected>(z => siteIds.Contains(z.SiteId))
                .Filter(filters)
                .OrderByDescending(z => z.VisitDate)
                .GroupBy(z => new { z.SiteId, z.VisitDate })
                .Select(z => new
                {
                    z.Key.SiteId,
                    z.Key.VisitDate,
                    Total = z.Sum(x => x.Commercial + x.Productive + x.Public + x.Residential)
                }).ToListAsync();

            return data
                .GroupBy(z => z.SiteId)
                .Select(z => z
                    .Where(x => x.VisitDate == z.OrderByDescending(z => z.VisitDate).First().VisitDate)
                    .Sum(z => z.Total)
                ).Sum();
        }

        #region Dispose
        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            { return; }

            if (disposing)
            {
                _repository?.Dispose();
            }

            _disposed = true;
        }
        #endregion
    }
}
