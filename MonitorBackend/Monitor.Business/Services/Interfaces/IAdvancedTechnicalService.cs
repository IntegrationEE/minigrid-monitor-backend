using System.Threading.Tasks;
using Monitor.Common.Models;

namespace Monitor.Business.Services
{
    public interface IAdvancedTechnicalService
    {
        Task<TechnicalViewModel> GetCharts(FilterParametersViewModel filters);
    }
}
