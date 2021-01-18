using System.Threading.Tasks;
using Monitor.Common.Models;

namespace Monitor.Business.Services
{
    public interface IAdvancedSocialService
    {
        Task<SocialViewModel> GetCharts(FilterParametersViewModel filters);
    }
}
