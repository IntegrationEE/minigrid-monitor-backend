using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Domain.Base;
using Monitor.Common.Models;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public interface IStateService
    {
        Task<IList<BaseLightModel>> GetAll();
        Task<IList<FilterLightModel>> GetListForFilters(UserInfo currentUser);
        Task<IList<StateMapModel>> GetListForMap();
    }
}
