using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Domain.Base;
using Monitor.Common.Models;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public interface ICompanyService : IBaseService<CompanyViewModel>
    {
        Task<IList<BaseLightModel>> GetList();
        Task<IList<FilterLightModel>> GetListForFilters(UserInfo currentUser);
    }
}
