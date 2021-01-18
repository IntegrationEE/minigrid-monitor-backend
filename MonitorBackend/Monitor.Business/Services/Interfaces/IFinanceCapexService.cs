using System.Threading.Tasks;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public interface IFinanceCapexService
    {
        Task<FinanceCapexViewModel> Get(int siteId);

        Task<FinanceCapexViewModel> Save(int siteId, FinanceCapexViewModel model);
    }
}
