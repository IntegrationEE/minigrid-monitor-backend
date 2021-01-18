using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Common.Models;

namespace Monitor.Business.Services
{
    public interface IProgrammeAnalyticsService
    {
        Task<List<TargetedChartViewModel>> GetData(int programmeId);
    }
}
