using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public interface IProgrammeIndicatorService
    {
        Task<IList<ProgrammeIndicatorLightModel>> GetByProgramme(int id);

        Task ToggleIsEnabled(int id);

        Task<ProgrammeIndicatorViewModel> Get(int id);

        Task<ProgrammeIndicatorViewModel> Create(ProgrammeIndicatorViewModel model);

        Task<ProgrammeIndicatorViewModel> Update(int id, ProgrammeIndicatorViewModel model);

        Task Delete(int id);
    }
}
