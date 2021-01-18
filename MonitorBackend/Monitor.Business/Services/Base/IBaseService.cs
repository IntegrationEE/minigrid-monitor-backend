using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Domain.Base;

namespace Monitor.Business.Services
{
    public interface IBaseService<TViewModel>
        where TViewModel : class, IBaseViewModel
    {
        Task<IList<TViewModel>> GetAll();

        Task<TViewModel> Get(int id);

        Task<TViewModel> Create(TViewModel model);

        Task<TViewModel> Update(int id, TViewModel model);

        Task Delete(int id);
    }
}
