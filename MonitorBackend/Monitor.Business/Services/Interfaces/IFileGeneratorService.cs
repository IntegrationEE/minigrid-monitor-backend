using System.Threading.Tasks;
using Monitor.Common.Enums;
using Monitor.Common.Models;

namespace Monitor.Business.Services
{
    public interface IFileGeneratorService
    {
        Task<byte[]> GetSocial(FilterParametersViewModel filters, SocialChartType chartType, FileFormat format);
        Task<byte[]> GetTechnical(FilterParametersViewModel filters, TechnicalChartType chartType, FileFormat format);
        Task<byte[]> GetFinancial(FilterParametersViewModel filters, FinancialChartType chartType, FileFormat format);
    }
}
