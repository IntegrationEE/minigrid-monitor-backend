using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Domain.Entities;

namespace Monitor.Business.Repositories
{
    public interface IChartConfigurationRepository
    {
        Task<IList<ChartConfiguration>> GetOverview();
        Task<IList<ChartConfiguration>> GetFinancial();
        Task<IList<ChartConfiguration>> GetTechnical();
        Task<IList<ChartConfiguration>> GetSocial();
    }
}
