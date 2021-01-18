using System;
using System.Linq;
using System.Linq.Expressions;
using Monitor.Domain.Base;
using Monitor.Common.Models;
using Monitor.Domain.Entities;

namespace Monitor.Business.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<Site> Filter(this IQueryable<Site> query, FilterParametersViewModel filters)
        {
            query = query.Where(z => z.IsPublished);

            if (filters.States.Count > 0)
            {
                query = query.Where(x => filters.States.Contains(x.StateId));
            }

            if (filters.Programmes.Count > 0)
            {
                query = query.Where(x => filters.Programmes.Contains(x.ProgrammeId));
            }

            if (filters.Developers.Count > 0)
            {
                query = query.Where(x => filters.Developers.Contains(x.CompanyId));
            }

            if (filters.From > 0)
            {
                query = query.Where(x => x.TechnicalParameter != null && x.TechnicalParameter.RenewableCapacity >= filters.From);
            }

            if (filters.To > 0)
            {
                query = query.Where(x => x.TechnicalParameter != null && x.TechnicalParameter.RenewableCapacity <= filters.To);
            }

            if (filters.SiteId.HasValue)
            {
                query = query.Where(x => x.Id == filters.SiteId.Value);
            }

            if (filters.Technologies.Count > 0)
            {
                Expression<Func<Site, bool>> exp = null;

                query = query.Where(z => z.TechnicalParameter != null);

                filters.Technologies.ForEach(technology =>
                {
                    exp = exp == null ?
                        x => x.TechnicalParameter.RenewableTechnology == technology :
                        exp.OR(z => z.TechnicalParameter.RenewableTechnology == technology);
                });

                query = query.Where(exp);
            }

            if (filters.GridConnections.Count > 0)
            {
                Expression<Func<Site, bool>> exp = null;

                query = query.Where(z => z.TechnicalParameter != null);

                filters.GridConnections.ForEach(gridConnection =>
                {
                    exp = exp == null ?
                        x => x.TechnicalParameter.GridConnection == gridConnection :
                        exp.OR(z => z.TechnicalParameter.GridConnection == gridConnection);
                });

                query = query.Where(exp);
            }

            return query;
        }

        public static IQueryable<SiteTechParameter> Filter(this IQueryable<SiteTechParameter> query, FilterParametersViewModel filters)
        {
            query = query.Where(z => z.Site.IsPublished);

            if (filters.States.Count > 0)
            {
                query = query.Where(x => filters.States.Contains(x.Site.StateId));
            }

            if (filters.Programmes.Count > 0)
            {
                query = query.Where(x => filters.Programmes.Contains(x.Site.ProgrammeId));
            }

            if (filters.Developers.Count > 0)
            {
                query = query.Where(x => filters.Developers.Contains(x.Site.CompanyId));
            }

            if (filters.From > 0)
            {
                query = query.Where(x => x.RenewableCapacity >= filters.From);
            }

            if (filters.To > 0)
            {
                query = query.Where(x => x.RenewableCapacity <= filters.To);
            }

            if (filters.SiteId.HasValue)
            {
                query = query.Where(x => x.SiteId == filters.SiteId.Value);
            }

            if (filters.Technologies.Count > 0)
            {
                Expression<Func<SiteTechParameter, bool>> exp = null;

                filters.Technologies.ForEach(technology =>
                {
                    exp = exp == null ?
                        x => x.RenewableTechnology == technology :
                        exp.OR(z => z.RenewableTechnology == technology);
                });

                query = query.Where(exp);
            }

            if (filters.GridConnections.Count > 0)
            {
                Expression<Func<SiteTechParameter, bool>> exp = null;

                filters.GridConnections.ForEach(gridConnection =>
                {
                    exp = exp == null ?
                        x => x.GridConnection == gridConnection :
                        exp.OR(z => z.GridConnection == gridConnection);
                });

                query = query.Where(exp);
            }

            return query;
        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> query, FilterParametersViewModel filters)
            where T : BaseSiteEntity
        {
            query = query.Where(z => z.Site.IsPublished);

            if (filters.States.Count > 0)
            {
                query = query.Where(x => filters.States.Contains(x.Site.StateId));
            }

            if (filters.Programmes.Count > 0)
            {
                query = query.Where(x => filters.Programmes.Contains(x.Site.ProgrammeId));
            }

            if (filters.Developers.Count > 0)
            {
                query = query.Where(x => filters.Developers.Contains(x.Site.CompanyId));
            }

            if (filters.From > 0)
            {
                query = query.Where(x => x.Site.TechnicalParameter != null && x.Site.TechnicalParameter.RenewableCapacity >= filters.From);
            }

            if (filters.To > 0)
            {
                query = query.Where(x => x.Site.TechnicalParameter != null && x.Site.TechnicalParameter.RenewableCapacity <= filters.To);
            }

            if (filters.SiteId.HasValue)
            {
                query = query.Where(x => x.SiteId == filters.SiteId.Value);
            }

            if (filters.Technologies.Count > 0)
            {
                Expression<Func<T, bool>> exp = null;

                query = query.Where(z => z.Site.TechnicalParameter != null);

                filters.Technologies.ForEach(technology =>
                {
                    exp = exp == null ?
                        x => x.Site.TechnicalParameter.RenewableTechnology == technology :
                        exp.OR(z => z.Site.TechnicalParameter.RenewableTechnology == technology);
                });

                query = query.Where(exp);
            }

            if (filters.GridConnections.Count > 0)
            {
                Expression<Func<T, bool>> exp = null;

                query = query.Where(z => z.Site.TechnicalParameter != null);

                filters.GridConnections.ForEach(gridConnection =>
                {
                    exp = exp == null ?
                        x => x.Site.TechnicalParameter.GridConnection == gridConnection :
                        exp.OR(z => z.Site.TechnicalParameter.GridConnection == gridConnection);
                });

                query = query.Where(exp);
            }

            return query;
        }
    }
}
