using System.Threading.Tasks;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public interface ISiteTechnicalParameterService
    {
        Task<SiteTechParameterViewModel> Get(int siteId);
        Task<SiteTechParameterViewModel> Save(int siteId, SiteTechParameterViewModel model);
    }
}
