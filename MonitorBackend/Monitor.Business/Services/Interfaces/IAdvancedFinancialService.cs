using System.Threading.Tasks;
using Monitor.Common.Models;

namespace Monitor.Business.Services
{
    public interface IAdvancedFinancialService
    {
        Task<FinancialViewModel> GetCharts(FilterParametersViewModel filters);
    }
}
