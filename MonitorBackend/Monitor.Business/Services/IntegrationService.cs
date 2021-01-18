using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public class IntegrationService : IIntegrationService
    {
        private readonly IBaseRepository _repository;

        public IntegrationService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<IntegrationLightModel>> GetAll()
        {
            using (_repository)
            {
                return await _repository.GetListWithOrder<IntegrationLightModel, Integration, object>(null, z => z.Name);
            }
        }

        public async Task<IntegrationViewModel> Get(int id)
        {
            using (_repository)
            {
                return await _repository.Get<IntegrationViewModel, Integration>(z => z.Id == id);
            }
        }

        public async Task<IntegrationViewModel> Create(IntegrationViewModel model)
        {
            using (_repository)
            {
                model.IsValid();

                var entity = new Integration();
                MapViewModel(model, entity);

                await _repository.Add(entity);
                await SaveSteps(model.Steps, entity);

                await _repository.SaveChanges();

                var response = await _repository.Get<IntegrationViewModel, Integration>(z => z.Id == entity.Id);

                if (response.IsActive && response.Steps.Count > 0)
                    response.TaskStatus = IntegrationTaskStatus.ADD;

                return response;
            }
        }

        public async Task<IntegrationViewModel> Update(int id, IntegrationViewModel model)
        {
            using (_repository)
            {
                model.IsValid();
                await CheckIfExists(id);

                var entity = await _repository.GetQuery<Integration>(x => x.Id == id, true)
                    .Include(z => z.Steps).ThenInclude(z => z.Records)
                    .FirstAsync();

                var stepsWereChange = model.WereStepsChanged(entity.Steps.OrderBy(z => z.Ordinal).Select(z => z.Function).ToList());

                var archiveSteps = entity.Steps.Where(z => !model.Steps.Select(z => z.Id).Contains(z.Id)).ToList();

                foreach (var step in archiveSteps)
                {
                    if (step.Records.Any())
                    {
                        step.MarkAsArchived();
                    }
                    else
                    {
                        _repository.Delete(step);
                        entity.Steps.Remove(step);
                    }
                }

                model.SetTaskStatus(entity.IsActive, entity.Interval, stepsWereChange, model.Steps.Count == 0);

                MapViewModel(model, entity);
                await SaveSteps(model.Steps, entity);

                await _repository.SaveChanges();

                return await _repository.Get<IntegrationViewModel, Integration>(z => z.Id == entity.Id);
            }
        }

        public async Task Delete(int id)
        {
            using (_repository)
            {
                await CheckIfExists(id);

                var entity = await _repository.GetQuery<Integration>(z => z.Id == id)
                    .Include(z => z.Records)
                    .Include(z => z.Steps)
                    .FirstAsync();

                if (entity.Records.Count > 0)
                {
                    _repository.Delete(entity.Records);
                }

                if (entity.Steps.Count > 0)
                {
                    _repository.Delete(entity.Steps);
                }

                _repository.Delete(entity);

                await _repository.SaveChanges();
            }
        }

        protected async Task CheckIfExists(int id)
        {
            if (!await _repository.Exists<Integration>(x => x.Id == id))
            {
                throw new CustomException($"Entity {nameof(Integration)} with id: '{id}' does not exist");
            }
        }

        private void MapViewModel(IntegrationViewModel model, Integration entity)
        {
            entity.Set(model.Name, model.Token, model.Interval, model.QuestionHash);
            entity.SetIsActive(model.IsActive);
        }

        private async Task SaveSteps(List<IntegrationStepViewModel> steps, Integration integration)
        {
            foreach (var step in steps)
            {
                var entity = integration.Steps.FirstOrDefault(z => z.Id == step.Id);

                entity ??= new IntegrationStep(integration);
                entity.Set(step.Name, step.Function, step.Ordinal);

                if (entity.Id == 0)
                {
                    await _repository.Add(entity);
                }
            }
        }
    }
}
