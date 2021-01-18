using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Common.Models;
using Monitor.Business.Common;

namespace Monitor.Business.Repositories
{
    public interface IAdvancedAnalyticsRepository : IDisposable
    {
        Task<IEnumerable<PeopleConnectedModel>> GetPeopleConnected(FilterParametersViewModel filters);

        Task<IEnumerable<EmploymentModel>> GetEmployment(FilterParametersViewModel filters);

        Task<IEnumerable<ServicesModel>> GetNewServices(FilterParametersViewModel filters);

        Task<CustomerSatisfactionModel> GetCustomerSatisfaction(FilterParametersViewModel filters);

        Task<IEnumerable<RevenueModel>> GetRevenues(FilterParametersViewModel filters);

        Task<CapexModel> GetCapex(FilterParametersViewModel filters);

        Task<OpexModel> GetOpex(FilterParametersViewModel filters);

        Task<FinanceModel> GetFinance(FilterParametersViewModel filters);

        Task<decimal> GetCapexPerConnections(FilterParametersViewModel filters);

        Task<decimal> GetOpexPerConnections(FilterParametersViewModel filters);

        Task<IEnumerable<AverageConsumptionModel>> GetAverageConsumption(FilterParametersViewModel filters);

        Task<List<CapacityUtilizationModel>> GetCapacityUtilization(FilterParametersViewModel filters);

        Task<IEnumerable<ElectricityConsumptionModel>> GetEletricityConsumption(FilterParametersViewModel filters);

        Task<List<InstalledCapacityModel>> GetInstalledCapacity(FilterParametersViewModel filters);
    }
}
