using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public interface IChartConfigurationService
    {
        Task<IList<ChartConfigurationViewModel>> GetAll();

        Task<ChartConfigurationViewModel> Update(int id, ChartConfigurationViewModel model);
    }
}
