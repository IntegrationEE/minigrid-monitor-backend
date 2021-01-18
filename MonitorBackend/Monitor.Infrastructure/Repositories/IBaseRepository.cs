using NLog;
using System;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Monitor.Domain.Base;

namespace Monitor.Infrastructure
{
    public interface IBaseRepository : IDisposable
    {
        Logger Logger { get; }

        IMapper Mapper { get; }

        Task<TViewModel> Get<TViewModel, TEntity>(Expression<Func<TEntity, bool>> expression)
            where TViewModel : class, IViewModel
            where TEntity : class, IBaseEntity;

        Task<IList<TViewModel>> GetList<TViewModel, TEntity>(Expression<Func<TEntity, bool>> expression = null)
            where TEntity : class, IBaseEntity
            where TViewModel : class, IViewModel;

        Task<IList<TViewModel>> GetListWithOrder<TViewModel, TEntity, TKey>(Expression<Func<TEntity, bool>> expression = null, params Expression<Func<TViewModel, TKey>>[] orderby)
            where TViewModel : class, IViewModel
            where TEntity : class, IBaseEntity;

        IQueryable<T> GetQuery<T>(Expression<Func<T, bool>> expression = null, bool trackChanges = false)
            where T : class, IEntity;

        Task Add<T>(T entity) where T : class, IEntity;

        Task AddRange<T>(T[] entity) where T : class, IEntity;

        void Update<T>(T entity) where T : class, IEntity;

        void Delete<T>(T entity) where T : class;

        void Delete<T>(ICollection<T> entities) where T : class;

        void Delete<T>(List<T> entities) where T : class;

        Task<bool> Exists<T>(Expression<Func<T, bool>> expression) where T : class;

        IQueryable<T> InterpolateSql<T>(FormattableString query) where T : class;

        Task SaveChanges(bool acceptAllChangesOnSuccess = true);
    }
}
