using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public interface IIntegrationService
    {
        Task<IList<IntegrationLightModel>> GetAll();

        Task<IntegrationViewModel> Get(int id);

        Task<IntegrationViewModel> Create(IntegrationViewModel model);

        Task<IntegrationViewModel> Update(int id, IntegrationViewModel model);

        Task Delete(int id);
    }
}
