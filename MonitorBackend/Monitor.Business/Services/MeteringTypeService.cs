using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public class MeteringTypeService : BaseService<MeteringTypeViewModel, MeteringType>, IMeteringTypeService
    {
        public MeteringTypeService(IBaseRepository repository)
            : base(repository)
        { }

        public override async Task<MeteringTypeViewModel> Create(MeteringTypeViewModel model)
        {
            using (Repository)
            {
                var entity = new MeteringType();
                MapViewModel(model, entity);

                await Repository.Add(entity);
                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        public override async Task<MeteringTypeViewModel> Update(int id, MeteringTypeViewModel model)
        {
            using (Repository)
            {
                await CheckIfExists(id);

                var entity = await Repository.GetQuery<MeteringType>(x => x.Id == id, true)
                    .FirstAsync();

                MapViewModel(model, entity);

                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        private void MapViewModel(MeteringTypeViewModel model, MeteringType entity)
        {
            entity.Set(model.Name);
        }
    }
}
