using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Common.Enums;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;

namespace Monitor.Business.Repositories
{
    public class ChartConfigurationRepository : IChartConfigurationRepository
    {
        private readonly IBaseRepository _repository;

        public ChartConfigurationRepository(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<ChartConfiguration>> GetOverview()
            => await _repository.GetQuery<ChartConfiguration>(z => z.Type == ChartType.OVERVIEW)
                .ToListAsync();

        public async Task<IList<ChartConfiguration>> GetFinancial()
            => await _repository.GetQuery<ChartConfiguration>(z =>
                    z.Type == ChartType.ADVANCED_ANALYTICS &&
                    (
                        z.Code == ChartCode.FINANCING_STRUCTURE ||
                        z.Code == ChartCode.CAPEX ||
                        z.Code == ChartCode.OPEX ||
                        z.Code == ChartCode.REVENUE
                    )
                ).ToListAsync();

        public async Task<IList<ChartConfiguration>> GetTechnical()
            => await _repository.GetQuery<ChartConfiguration>(z =>
                    z.Type == ChartType.ADVANCED_ANALYTICS &&
                    (
                        z.Code == ChartCode.INSTALLED_CAPACITY ||
                        z.Code == ChartCode.ELECTRICITY_CONSUMPTION ||
                        z.Code == ChartCode.CAPACITY_UTILIZATION ||
                        z.Code == ChartCode.AVERAGE_CONSUMPTIONS ||
                        z.Code == ChartCode.DAILY_PROFILE
                    )
                ).ToListAsync();

        public async Task<IList<ChartConfiguration>> GetSocial()
            => await _repository.GetQuery<ChartConfiguration>(z =>
                    z.Type == ChartType.ADVANCED_ANALYTICS &&
                    (
                        z.Code == ChartCode.CONNECTIONS ||
                        z.Code == ChartCode.NEW_SERVICES ||
                        z.Code == ChartCode.EMPLOYMENTS ||
                        z.Code == ChartCode.CUSTOMER_SATISFACTION
                    )
                ).ToListAsync();
    }
}
