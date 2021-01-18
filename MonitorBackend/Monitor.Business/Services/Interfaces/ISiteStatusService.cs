using System.Threading.Tasks;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public interface ISiteStatusService
    {
        Task<SiteStatusModel> GetStatus(int id);
    }
}
