using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Common;
using Monitor.Domain.Base;
using Monitor.Infrastructure;

namespace Monitor.Business.Services
{
    public class BaseService<TViewModel, TEntity> : IBaseService<TViewModel>
        where TViewModel : class, IBaseViewModel
        where TEntity : class, IBaseEntity
    {
        protected readonly IBaseRepository Repository;

        public BaseService(IBaseRepository repository)
        {
            Repository = repository;
        }

        public virtual async Task<IList<TViewModel>> GetAll()
        {
            return await Repository.GetList<TViewModel, TEntity>();
        }

        public virtual async Task<TViewModel> Get(int id)
        {
            return await GetViewModel(id);
        }

        public virtual Task<TViewModel> Create(TViewModel model)
        {
            throw new NotImplementedException();
        }

        public virtual Task<TViewModel> Update(int id, TViewModel model)
        {
            throw new NotImplementedException();
        }

        public virtual async Task Delete(int id)
        {
            await CheckIfExists(id);

            var entity = Activator.CreateInstance<TEntity>();
            entity.Id = id;

            Repository.Delete(entity);
            await Repository.SaveChanges();
        }

        protected async Task<TViewModel> GetViewModel(int id) => await Repository.Get<TViewModel, TEntity>(x => x.Id == id);

        protected async Task CheckIfExists(int id)
        {
            if (!await Repository.Exists<TEntity>(x => x.Id == id))
            {
                throw new CustomException($"Entity {nameof(TEntity)} with id: '{id}' does not exist");
            }
        }
    }
}
