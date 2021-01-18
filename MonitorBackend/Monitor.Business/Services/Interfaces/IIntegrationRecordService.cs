using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public interface IIntegrationRecordService
    {
        Task<IList<IntegrationRecordLightModel>> GetAll(int integrationId);
    }
}
