using System.Threading.Tasks;

namespace Monitor.Business.Repositories
{
    public interface ISettingRepository
    {
        Task<string> GetCurrency();
    }
}
