using System.Threading.Tasks;
using Monitor.Common.Models;

namespace Monitor.Business.Services
{
    public interface IPortfolioService
    {
        Task<PortfolioViewModel> GetData(FilterParametersViewModel filters);
    }
}
