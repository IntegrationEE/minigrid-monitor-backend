using System.Threading.Tasks;
using Monitor.Common.Models;

namespace Monitor.Business.Services
{
    public interface IGraphService
    {
        Task<SiteGraphViewModel> Get(int id);
    }
}
