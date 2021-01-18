using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public interface ISettingService
    {
        Task<List<SettingViewModel>> GetAllForFrontend();
    }
}
