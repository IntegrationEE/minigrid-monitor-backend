using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Monitor.Common;
using Monitor.Domain.Base;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Infrastructure;

namespace Monitor.Business.Services
{
    public abstract class BaseYearMonthIndicatorService<TViewModel, TEntity, TQuartile> : IBaseYearMonthIndicatorService<TViewModel>
        where TViewModel : BaseYearMonthIndicatorModel
        where TEntity : class, IBaseYearMonthIndicatorEntity
        where TQuartile : class, IQuartile
    {
        protected readonly IBaseRepository Repository;
        protected readonly DocumentGenerator DocumentGenerator;
        protected string[] ApplicableHeaders;

        public BaseYearMonthIndicatorService(IBaseRepository repository)
        {
            Repository = repository;
            DocumentGenerator = new DocumentGenerator();
        }

        public abstract Task<List<TViewModel>> GetAll(int parentId);

        public async Task<YearMonthIndicatorUploadResponse> Upload(int id, IFormFile file)
        {
            var response = new YearMonthIndicatorUploadResponse();

            using (Repository)
            {
                var data = GetDataFromFile(file);

                var entities = await GetAllByYearAndMonth(id, data.Select(z => z.Year).ToArray(), data.Select(z => z.Month).ToArray());

                foreach (var item in data)
                {
                    if (item.Month < 1 || item.Month > 12)
                    { throw new CustomException($"The given Year/Month combination doesn’t exist (Year: '{item.Year}', Month: '{item.Month}')."); }

                    var entity = entities.FirstOrDefault(z => z.Year == item.Year && z.Month == item.Month);

                    if (!item.IsApplicable())
                    {
                        response.NotApplicable++;
                        continue;
                    }

                    entity = CreateOrUpdateEntity(id, entity, item);

                    if (entity.Id == 0)
                    {
                        response.Inserted++;
                        await Repository.Add(entity);
                    }
                    else
                    {
                        response.Updated++;
                        Repository.Update(entity);
                    }
                }

                await Repository.SaveChanges();

                var quartile = await GetQuartiles(id);

                foreach (var item in data)
                {
                    var outlier = ValidateOutliersProperties(quartile, item);

                    if (outlier != null)
                    {
                        response.Outliers.Add(outlier);
                    }
                }
            }

            return response;
        }

        public abstract Task<TViewModel> Create(int parentId, TViewModel model);

        public abstract Task<TViewModel> Update(int id, TViewModel model);

        public async Task<YearMonthIndicatorValidateResponse> ValidateOutliers(int parentId, TViewModel model)
        {
            using (Repository)
            {
                var quartile = await GetQuartiles(parentId);
                var outliers = quartile != null ? ValidateOutliersProperties(quartile, model) : null;

                return new YearMonthIndicatorValidateResponse(outliers);
            }
        }

        public byte[] GenerateDocument(List<TViewModel> models, FileFormat format)
        {
            var fileFormat = DocumentGenerator.GetFileFormat(format);

            return DocumentGenerator.Generate(models, GetHeaders().Select(z => z.title).ToArray(), fileFormat);
        }

        protected async Task<TViewModel> GetViewModel(int id) => await Repository.Get<TViewModel, TEntity>(x => x.Id == id);

        protected IList<TViewModel> GetDataFromFile(IFormFile file)
            => DocumentGenerator.GetDataFromExcel<TViewModel>(file, GetHeaders());

        protected void CheckIfIsApplicable(TViewModel model)
        {
            if (!model.IsApplicable())
            { throw new CustomException($"Please fill at least one of {string.Join(", ", ApplicableHeaders)}."); }
        }

        protected async Task CheckIfExists(int id)
        {
            if (!await Repository.Exists<TEntity>(x => x.Id == id))
            {
                throw new CustomException($"Entity {nameof(TEntity)} with id: '{id}' does not exist");
            }
        }

        protected abstract Task<IList<TEntity>> GetAllByYearAndMonth(int id, int[] years, int[] months);

        protected abstract TEntity CreateOrUpdateEntity(int parentId, TEntity entity, TViewModel model);

        protected abstract Task<TQuartile> GetQuartiles(int parentId);

        protected abstract Outlier ValidateOutliersProperties(TQuartile quartile, TViewModel model);

        private (string title, bool required)[] GetHeaders()
            => new (string title, bool required)[] {
                (ChartConstans.YEAR, true),
                (ChartConstans.MONTH, true)
            }
            .Concat(ApplicableHeaders.Select(z => (z, false)))
            .ToArray();
    }
}
