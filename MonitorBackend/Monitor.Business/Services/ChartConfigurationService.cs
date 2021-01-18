using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Common;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public class ChartConfigurationService : IChartConfigurationService
    {
        private readonly IBaseRepository _repository;

        public ChartConfigurationService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<ChartConfigurationViewModel>> GetAll()
        {
            using (_repository)
            {
                return await _repository.GetListWithOrder<ChartConfigurationViewModel, ChartConfiguration, object>(null, x => x.Code);
            }
        }

        public async Task<ChartConfigurationViewModel> Update(int id, ChartConfigurationViewModel model)
        {
            model.IsValid();

            using (_repository)
            {
                var entity = await _repository.GetQuery<ChartConfiguration>(z => z.Id == id, true)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException($"Chart configuration with id: '{id}' does not exist"); }

                entity.Set(model.Title, model.Tooltip, model.UnitOfMeasure, model.IsCumulative, model.Convertable);

                await _repository.SaveChanges();

                return await _repository.Get<ChartConfigurationViewModel, ChartConfiguration>(z => z.Id == id);
            }
        }
    }
}
