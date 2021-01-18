using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public interface IThresholdService
    {
        Task<IList<ThresholdViewModel>> GetAll();
        Task<ThresholdViewModel> Update(ThresholdViewModel model);
    }
}
