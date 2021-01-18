using NLog;
using System;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Monitor.Domain.Base;

namespace Monitor.Infrastructure
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly MinigridDbContext Context;

        public IMapper Mapper { get; }

        public Logger Logger { get; }

        public BaseRepository(MinigridDbContext context)
        {
            Context = context;
            Logger = LogManager.GetCurrentClassLogger();
        }

        public BaseRepository(MinigridDbContext context, IMapper mapper)
            : this(context)
        {
            Mapper = mapper;
        }

        public async Task<TViewModel> Get<TViewModel, TEntity>(Expression<Func<TEntity, bool>> expression)
            where TViewModel : class, IViewModel
            where TEntity : class, IBaseEntity
        {
            var query = SetExpression(expression);

            return await query
                .ProjectTo<TViewModel>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<IList<TViewModel>> GetList<TViewModel, TEntity>(Expression<Func<TEntity, bool>> expression = null)
            where TViewModel : class, IViewModel
            where TEntity : class, IBaseEntity
        {
            return await GetListWithOrder<TViewModel, TEntity, object>(expression);
        }

        public async Task<IList<TViewModel>> GetListWithOrder<TViewModel, TEntity, TKey>(Expression<Func<TEntity, bool>> expression = null, params Expression<Func<TViewModel, TKey>>[] orderby)
            where TViewModel : class, IViewModel
            where TEntity : class, IBaseEntity
        {
            var query = SetExpression(expression);

            var viewQuery = query.ProjectTo<TViewModel>(Mapper.ConfigurationProvider);

            if (orderby != null && orderby.Length > 0)
            {
                IOrderedQueryable<TViewModel> orderedQuery = null;

                for (var i = 0; i < orderby.Length; i++)
                {
                    orderedQuery = i == 0 ? viewQuery.OrderBy(orderby[i]) : orderedQuery.ThenBy(orderby[i]);
                }

                viewQuery = orderedQuery.AsQueryable();
            }

            return await viewQuery.ToListAsync();
        }

        public IQueryable<T> GetQuery<T>(Expression<Func<T, bool>> expression = null, bool trackChanges = false)
            where T : class, IEntity
        {
            return SetExpression(expression, trackChanges);
        }

        public async Task Add<T>(T entity) where T : class, IEntity
        {
            await Context.AddAsync(entity);
        }

        public async Task AddRange<T>(T[] entity) where T : class, IEntity
        {
            await Context.AddRangeAsync(entity);
        }

        public void Update<T>(T entity) where T : class, IEntity
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Context.Attach(entity);
            }

            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class
        {
            Context.Remove(entity);
        }

        public void Delete<T>(ICollection<T> entities) where T : class
        {
            Context.RemoveRange(entities);
        }

        public void Delete<T>(List<T> entities) where T : class
        {
            Context.RemoveRange(entities);
        }

        public async Task<bool> Exists<T>(Expression<Func<T, bool>> expression)
            where T : class
        {
            return await Context.Set<T>().AsNoTracking().AnyAsync(expression);
        }

        public IQueryable<T> InterpolateSql<T>(FormattableString query)
            where T : class
        {
            return Context.Set<T>().FromSqlInterpolated(query);
        }

        public async Task SaveChanges(bool acceptAllChangesOnSuccess = true)
        {
            await Context.SaveChangesAsync(acceptAllChangesOnSuccess: acceptAllChangesOnSuccess);
        }

        private IQueryable<TEntity> SetExpression<TEntity>(Expression<Func<TEntity, bool>> expression, bool trackChanges = false)
            where TEntity : class, IEntity
        {
            var query = Context.Set<TEntity>().AsQueryable();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (!trackChanges)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        #region Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                Context.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}