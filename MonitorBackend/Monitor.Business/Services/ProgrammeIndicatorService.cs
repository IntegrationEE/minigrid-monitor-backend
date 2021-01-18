using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Common;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public class ProgrammeIndicatorService : IProgrammeIndicatorService
    {
        private readonly IBaseRepository _repository;
        private const int _maxEnabledIndicators = 6;

        public ProgrammeIndicatorService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProgrammeIndicatorViewModel> Get(int id)
        {
            using (_repository)
            {
                return await _repository.Get<ProgrammeIndicatorViewModel, ProgrammeIndicator>(x => x.Id == id);
            }
        }

        public async Task<IList<ProgrammeIndicatorLightModel>> GetByProgramme(int id)
        {
            using (_repository)
            {
                return await _repository.GetListWithOrder<ProgrammeIndicatorLightModel, ProgrammeIndicator, bool>(
                    x => x.ProgrammeId == id, z => z.IsEnabled
                );
            }
        }

        public async Task ToggleIsEnabled(int id)
        {
            using (_repository)
            {
                var entity = await _repository.GetQuery<ProgrammeIndicator>(z => z.Id == id, true)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException("Programme's indicator doesn't exist."); }

                if (!entity.IsEnabled)
                {
                    var countOfEnabled = await _repository.GetQuery<ProgrammeIndicator>(z => z.ProgrammeId == entity.ProgrammeId && z.IsEnabled)
                        .CountAsync();

                    if (countOfEnabled >= _maxEnabledIndicators)
                    { throw new CustomException("You have reached maximum number of enabled Programme's indicators."); }
                }

                entity.ToggleIsEnabled();

                await _repository.SaveChanges();
            }
        }

        public async Task<ProgrammeIndicatorViewModel> Create(ProgrammeIndicatorViewModel model)
        {
            model.IsValid();

            using (_repository)
            {
                var entity = new ProgrammeIndicator(model.ProgrammeId);

                MapViewModel(entity, model);

                await _repository.Add(entity);
                await _repository.SaveChanges();

                return await _repository.Get<ProgrammeIndicatorViewModel, ProgrammeIndicator>(z => z.Id == entity.Id);
            }
        }

        public async Task<ProgrammeIndicatorViewModel> Update(int id, ProgrammeIndicatorViewModel model)
        {
            model.IsValid();

            using (_repository)
            {
                var entity = await _repository.GetQuery<ProgrammeIndicator>(z => z.Id == id, true)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException("Programme's indicator doesn't exist."); }

                MapViewModel(entity, model);

                await _repository.SaveChanges();

                return await _repository.Get<ProgrammeIndicatorViewModel, ProgrammeIndicator>(z => z.Id == id);
            }
        }

        public async Task Delete(int id)
        {
            using (_repository)
            {
                var entity = await _repository.GetQuery<ProgrammeIndicator>(x => x.Id == id)
                    .Include(z => z.Values)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException("Programme's indicator doesn't exist."); }

                if (entity.Values.Count > 0)
                { _repository.Delete(entity.Values); }

                _repository.Delete(entity);

                await _repository.SaveChanges();
            }
        }

        private void MapViewModel(ProgrammeIndicator entity, ProgrammeIndicatorViewModel model)
        {
            entity.Set(model.Name, model.Unit, model.Description, model.Target, model.IsCumulative);
        }
    }
}
