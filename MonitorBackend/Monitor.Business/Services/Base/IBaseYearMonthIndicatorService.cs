using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Monitor.Domain.Base;
using Monitor.Common.Enums;
using Monitor.Common.Models;

namespace Monitor.Business.Services
{
    public interface IBaseYearMonthIndicatorService<TViewModel>
        where TViewModel : BaseYearMonthIndicatorModel
    {
        Task<List<TViewModel>> GetAll(int parentId);

        Task<YearMonthIndicatorUploadResponse> Upload(int parentId, IFormFile file);

        Task<TViewModel> Create(int parentId, TViewModel model);

        Task<TViewModel> Update(int id, TViewModel model);

        Task<YearMonthIndicatorValidateResponse> ValidateOutliers(int parentId, TViewModel model);

        byte[] GenerateDocument(List<TViewModel> models, FileFormat format);
    }
}
