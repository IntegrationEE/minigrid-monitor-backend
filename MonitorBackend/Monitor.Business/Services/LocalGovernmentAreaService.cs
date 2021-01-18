using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Monitor.Domain.Base;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public class LocalGovernmentAreaService : BaseService<LocalGovernmentAreaViewModel, LocalGovernmentArea>, ILocalGovernmentAreaService
    {
        public LocalGovernmentAreaService(IBaseRepository repository)
            : base(repository)
        { }

        public async Task<IList<BaseLightModel>> GetByState(int stateId)
        {
            using (Repository)
            {
                return await Repository.GetQuery<LocalGovernmentArea>(x => x.StateId == stateId)
                    .OrderBy(x => x.Name)
                    .ProjectTo<BaseLightModel>(Repository.Mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }

        public override async Task<LocalGovernmentAreaViewModel> Create(LocalGovernmentAreaViewModel model)
        {
            using (Repository)
            {
                model.IsValid();

                var entity = new LocalGovernmentArea();
                MapViewModel(model, entity);

                await Repository.Add(entity);
                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        public override async Task<LocalGovernmentAreaViewModel> Update(int id, LocalGovernmentAreaViewModel model)
        {
            using (Repository)
            {
                model.IsValid();
                await CheckIfExists(id);

                var entity = await Repository.GetQuery<LocalGovernmentArea>(x => x.Id == id, true)
                    .FirstAsync();

                MapViewModel(model, entity);

                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        private void MapViewModel(LocalGovernmentAreaViewModel model, LocalGovernmentArea entity)
        {
            entity.Set(model.Name, model.StateId);
        }
    }
}
