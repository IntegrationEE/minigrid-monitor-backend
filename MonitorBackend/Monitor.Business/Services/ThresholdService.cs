using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public class ThresholdService : IThresholdService
    {
        private readonly IBaseRepository _repository;

        public ThresholdService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<ThresholdViewModel>> GetAll()
        {
            using (_repository)
            {
                return await _repository.GetListWithOrder<ThresholdViewModel, Threshold, object>(null, x => x.Code);
            }
        }

        public async Task<ThresholdViewModel> Update(ThresholdViewModel model)
        {
            model.IsValid();

            using (_repository)
            {
                var entity = await _repository.GetQuery<Threshold>(x => x.Code == model.Code, true)
                    .FirstAsync();

                entity.Set(model.Min, model.Max);

                await _repository.SaveChanges();

                return await _repository.Get<ThresholdViewModel, Threshold>(x => x.Code == model.Code);
            }
        }
    }
}
