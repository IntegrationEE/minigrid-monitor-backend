using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Common.Models;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public interface ISiteService
    {
        Task<IList<SiteDashboardModel>> GetFilteredSite(FilterParametersViewModel filters);

        Task<IList<SiteLightModel>> GetList(UserInfo currentUser);

        Task<IList<SiteCardModel>> GetByCurrent(UserInfo currentUser);

        Task<SiteViewModel> Get(int id);

        Task<SiteViewModel> Create(SiteViewModel model);

        Task<SiteViewModel> Update(int id, SiteViewModel model);

        Task ToggleIsPublished(int id);

        Task Delete(int id);
    }
}
