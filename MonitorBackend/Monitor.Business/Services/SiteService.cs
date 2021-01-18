using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;
using Monitor.Business.Extensions;

namespace Monitor.Business.Services
{
    public class SiteService : ISiteService
    {
        private readonly IBaseRepository _repository;

        public SiteService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<SiteDashboardModel>> GetFilteredSite(FilterParametersViewModel filters)
        {
            using (_repository)
            {
                return await _repository.GetQuery<Site>()
                    .Filter(filters)
                    .OrderByDescending(x => x.TechnicalParameter != null ? x.TechnicalParameter.RenewableCapacity : 0)
                    .ProjectTo<SiteDashboardModel>(_repository.Mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }

        public async Task<IList<SiteCardModel>> GetByCurrent(UserInfo currentUser)
        {
            using (_repository)
            {
                var exp = await GetFilter(currentUser);

                return await _repository.GetListWithOrder<SiteCardModel, Site, string>(exp, x => x.Name);
            }
        }

        public async Task<IList<SiteLightModel>> GetList(UserInfo currentUser)
        {
            using (_repository)
            {
                var exp = await GetFilter(currentUser);

                if (exp != null)
                    exp = exp.AND(z => z.IsPublished);
                else
                    exp = x => x.IsPublished;

                return await _repository.GetListWithOrder<SiteLightModel, Site, string>(exp, x => x.Name);
            }
        }

        public async Task<SiteViewModel> Get(int id)
        {
            using (_repository)
            {
                return await GetViewModel(id);
            }
        }

        public async Task<SiteViewModel> Create(SiteViewModel model)
        {
            using (_repository)
            {
                var entity = new Site();
                MapViewModel(model, entity);
                entity.UpdateQrCode();

                await _repository.Add(entity);
                await _repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        public async Task<SiteViewModel> Update(int id, SiteViewModel model)
        {
            using (_repository)
            {
                var entity = await _repository.GetQuery<Site>(x => x.Id == id, true)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException($"Entity {nameof(Site)} with id: '{id}' does not exist"); }

                MapViewModel(model, entity);

                await _repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        public async Task ToggleIsPublished(int id)
        {
            using (_repository)
            {
                var entity = await _repository.GetQuery<Site>(x => x.Id == id, true)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException($"Entity {nameof(Site)} with id: '{id}' does not exist"); }

                entity.ToggleIsPublished();

                await _repository.SaveChanges();
            }
        }

        public async Task Delete(int id)
        {
            using (_repository)
            {
                var entity = await _repository.GetQuery<Site>(x => x.Id == id)
                    .Include(x => x.Tariffs)
                    .Include(x => x.Revenues)
                    .Include(z => z.Employments)
                    .Include(z => z.NewServices)
                    .Include(x => x.FinanceOpex)
                    .Include(x => x.FinanceCapex)
                    .Include(x => x.Consumptions)
                    .Include(x => x.PeopleConnected)
                    .Include(x => x.TechnicalParameter)
                    .Include(x => x.CustomerSatisfactions)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException($"Entity {nameof(Site)} with id: '{id}' does not exist"); }

                if (entity.Consumptions.Count > 0)
                { _repository.Delete(entity.Consumptions); }

                if (entity.CustomerSatisfactions.Count > 0)
                { _repository.Delete(entity.CustomerSatisfactions); }

                if (entity.Employments.Count > 0)
                { _repository.Delete(entity.Employments); }

                if (entity.FinanceCapex != null)
                { _repository.Delete(entity.FinanceCapex); }

                if (entity.FinanceOpex.Count > 0)
                { _repository.Delete(entity.FinanceOpex); }

                if (entity.NewServices.Count > 0)
                { _repository.Delete(entity.NewServices); }

                if (entity.PeopleConnected.Count > 0)
                { _repository.Delete(entity.PeopleConnected); }

                if (entity.Revenues.Count > 0)
                { _repository.Delete(entity.Revenues); }

                if (entity.Tariffs.Count > 0)
                { _repository.Delete(entity.Tariffs); }

                if (entity.TechnicalParameter != null)
                { _repository.Delete(entity.TechnicalParameter); }

                _repository.Delete(entity);

                await _repository.SaveChanges();
            }
        }

        private void MapViewModel(SiteViewModel model, Site entity)
        {
            entity.Set(model.Name, model.StateId, model.ProgrammeId, model.CompanyId, model.Lat,
                model.Long, model.CommissioningDate);
        }

        private async Task<SiteViewModel> GetViewModel(int id)
            => await _repository.Get<SiteViewModel, Site>(x => x.Id == id);

        private async Task<Expression<Func<Site, bool>>> GetFilter(UserInfo currentUser)
        {
            UserViewModel user = null;

            if (currentUser.Id > 0 && currentUser.Role == RoleCode.PROGRAMME_MANAGER)
            {
                user = await _repository.GetQuery<User>(z => z.Id == currentUser.Id)
                    .Select(z => new UserViewModel { CompanyId = z.CompanyId, Programmes = z.Programmes.Select(x => x.ProgrammeId) })
                    .FirstAsync();
            }

            Expression<Func<Site, bool>> exp = null;

            exp = currentUser.Role switch
            {
                RoleCode.DEVELOPER => x => x.CompanyId == currentUser.CompanyId,
                RoleCode.PROGRAMME_MANAGER => x => user.Programmes.Any(z => z == x.ProgrammeId),
                _ => null,
            };

            return exp;
        }
    }
}
