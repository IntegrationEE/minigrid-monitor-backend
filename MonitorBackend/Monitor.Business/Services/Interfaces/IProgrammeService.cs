using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Common.Models;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public interface IProgrammeService : IBaseService<ProgrammeViewModel>
    {
        Task<IList<FilterLightModel>> GetListForFilters(UserInfo currentUser);
        Task<IList<ProgrammeViewModel>> GetByCurrent(UserInfo currentUser);
    }
}
