using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Domain.Base;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public class StateService : IStateService
    {
        private readonly IBaseRepository _repository;

        public StateService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<BaseLightModel>> GetAll()
        {
            using (_repository)
            {
                return await _repository.GetListWithOrder<BaseLightModel, State, object>(null, x => x.Name);
            }
        }

        public async Task<IList<StateMapModel>> GetListForMap()
        {
            using (_repository)
            {
                var response = new List<StateMapModel>();

                var states = await _repository.GetQuery<State>()
                    .Select(z => new
                    {
                        z.Id,
                        z.Name,
                        z.BorderLine.Geometries
                    })
                    .OrderBy(z => z.Name)
                    .ToListAsync();

                foreach (var state in states)
                {
                    var item = new StateMapModel
                    {
                        Id = state.Id,
                        Name = state.Name
                    };

                    if (state.Geometries == null)
                    { continue; }

                    foreach (var geometry in state.Geometries)
                    {
                        double[,] coordinates = new double[geometry.Coordinates.Length, 2];
                        var i = 0;

                        foreach (var coordinate in geometry.Coordinates)
                        {
                            coordinates[i, 0] = coordinate.X;
                            coordinates[i++, 1] = coordinate.Y;
                        }

                        item.Coordinates.Add(coordinates);
                    }

                    response.Add(item);
                }

                return response;
            }
        }

        public async Task<IList<FilterLightModel>> GetListForFilters(UserInfo currentUser)
        {
            using (_repository)
            {
                Expression<Func<State, bool>> stateExpression = null;
                Expression<Func<Site, bool>> siteExpression = null;

                switch (currentUser.Role)
                {
                    case RoleCode.DEVELOPER:
                        stateExpression = x => x.Sites.Any(z => z.CompanyId == currentUser.CompanyId);
                        siteExpression = x => x.CompanyId == currentUser.CompanyId;
                        break;
                    default:
                        stateExpression = x => true;
                        siteExpression = x => true;
                        break;
                }

                return await _repository.GetQuery<State>()
                    .Where(stateExpression)
                    .Select(x => new FilterLightModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        SiteIds = x.Sites.AsQueryable().Where(siteExpression).Select(z => z.Id)
                    })
                    .OrderBy(x => x.Name)
                    .ToListAsync();
            }
        }
    }
}
