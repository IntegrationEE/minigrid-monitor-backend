using System.Collections.Generic;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public interface IEnumService
    {
        List<EnumViewModel<T>> GetList<T>()
            where T : struct;
    }
}
