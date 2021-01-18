using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Monitor.Common;
using Monitor.Common.Models;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.Quartiles;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public class FinanceOpexService : BaseYearMonthIndicatorService<FinanceOpexViewModel, FinanceOpex, FinanceOpexQuartile>, IFinanceOpexService
    {
        public FinanceOpexService(IBaseRepository repository)
            : base(repository)
        {
            ApplicableHeaders = new string[]
            {
                ChartConstans.SITE_SPECIFIC,
                ChartConstans.COMPANY_LEVEL,
                ChartConstans.TAXES,
                ChartConstans.LOAN_REPAYMENTS,
            };
        }

        public override async Task<List<FinanceOpexViewModel>> GetAll(int siteId)
        {
            using (Repository)
            {
                return await Repository
                    .GetQuery<FinanceOpex>(entity => entity.SiteId == siteId)
                    .OrderByDescending(x => x.Year).ThenByDescending(x => x.Month)
                    .ProjectTo<FinanceOpexViewModel>(Repository.Mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }

        public override async Task<FinanceOpexViewModel> Create(int siteId, FinanceOpexViewModel model)
        {
            CheckIfIsApplicable(model);
            model.IsValid();

            using (Repository)
            {
                await CheckIfDuplicated(siteId, model);

                var entity = CreateOrUpdateEntity(siteId, null, model);

                await Repository.Add(entity);
                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        public override async Task<FinanceOpexViewModel> Update(int id, FinanceOpexViewModel model)
        {
            CheckIfIsApplicable(model);
            model.IsValid();

            using (Repository)
            {
                await CheckIfExists(id);

                var entity = await Repository.GetQuery<FinanceOpex>(x => x.Id == id, true)
                    .FirstAsync();

                MapViewModel(model, entity);

                await Repository.SaveChanges();

                return await GetViewModel(entity.Id);
            }
        }

        protected override Outlier ValidateOutliersProperties(FinanceOpexQuartile quartile, FinanceOpexViewModel model)
        {
            var outliers = new Outlier(model.Year, model.Month);

            if (quartile.CompanyLevelMinMax.IsOutlier(model.CompanyLevel))
            { outliers.AddProperty(nameof(model.CompanyLevel)); }

            if (quartile.LoanRepaymentsMinMax.IsOutlier(model.LoanRepayments))
            { outliers.AddProperty(nameof(model.LoanRepayments)); }

            if (quartile.SiteSpecificMinMax.IsOutlier(model.SiteSpecific))
            { outliers.AddProperty(nameof(model.SiteSpecific)); }

            if (quartile.TaxesMinMax.IsOutlier(model.Taxes))
            { outliers.AddProperty(nameof(model.Taxes)); }

            return outliers.Properties.Count > 0 ? outliers : null;
        }

        protected override async Task<IList<FinanceOpex>> GetAllByYearAndMonth(int id, int[] years, int[] months)
            => await Repository
                .GetQuery<FinanceOpex>(z => z.SiteId == id && years.Contains(z.Year) && months.Contains(z.Month))
                .ToListAsync();

        protected override async Task<FinanceOpexQuartile> GetQuartiles(int siteId)
            => await Repository
                .InterpolateSql<FinanceOpexQuartile>($"SELECT * FROM getFinanceOpexQuartiles({siteId})")
                .FirstOrDefaultAsync();

        protected override FinanceOpex CreateOrUpdateEntity(int siteId, FinanceOpex entity, FinanceOpexViewModel model)
        {
            entity ??= new FinanceOpex(siteId, model.Year, model.Month);
            MapViewModel(model, entity);

            return entity;
        }

        private void MapViewModel(FinanceOpexViewModel model, FinanceOpex entity)
        {
            entity.Set(model.SiteSpecific, model.CompanyLevel, model.Taxes, model.LoanRepayments);
        }

        private async Task CheckIfDuplicated(int parentId, FinanceOpexViewModel model)
        {
            if (await Repository.Exists<FinanceOpex>(z => z.SiteId == parentId && z.Year == model.Year && z.Month == model.Month))
            {
                throw new CustomException($"Exists Finance Opex for selected Year: '{model.Year}' and Month: '{model.Month}'.");
            }
        }
    }
}
