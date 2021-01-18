using System.Threading.Tasks;

namespace Monitor.Business.Services
{
    public interface ISiteQrService
    {
        Task<byte[]> Get(int id);

        Task Update(int id);
    }
}
