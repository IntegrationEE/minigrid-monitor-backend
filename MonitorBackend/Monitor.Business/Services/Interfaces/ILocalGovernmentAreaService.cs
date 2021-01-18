using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Domain.Base;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public interface ILocalGovernmentAreaService : IBaseService<LocalGovernmentAreaViewModel>
    {
        Task<IList<BaseLightModel>> GetByState(int stateId);
    }
}
