using System;
using System.Threading.Tasks;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Business.Repositories;

namespace Monitor.Business.Services
{
    public class FileGeneratorService : IFileGeneratorService
    {
        private readonly IAdvancedAnalyticsRepository _repository;
        private readonly DocumentGenerator _documentGenerator;

        public FileGeneratorService(IAdvancedAnalyticsRepository repository)
        {
            _repository = repository;
            _documentGenerator = new DocumentGenerator();
        }

        public async Task<byte[]> GetSocial(FilterParametersViewModel filters, SocialChartType chartType, FileFormat format)
        {
            byte[] response = null;
            var fileFormat = _documentGenerator.GetFileFormat(format);

            using (_repository)
            {
                switch (chartType)
                {
                    case SocialChartType.PEOPLE_CONNECTED:
                        var peopleConnectedData = await _repository.GetPeopleConnected(filters);
                        response = _documentGenerator.Generate(
                            peopleConnectedData,
                            new[] { ChartConstans.YEAR, ChartConstans.COMMERCIAL, ChartConstans.PRODUCTIVE, ChartConstans.RESIDENTIAL, ChartConstans.PUBLIC },
                            fileFormat
                        );
                        break;
                    case SocialChartType.EMPLOYMENTS:
                        var employmentsData = await _repository.GetEmployment(filters);
                        response = _documentGenerator.Generate(
                            employmentsData,
                            new[] { ChartConstans.YEAR, ChartConstans.INDIRECT, ChartConstans.DIRECT },
                            fileFormat
                        );
                        break;
                    case SocialChartType.NEW_SERVICES:
                        var newServicesData = await _repository.GetNewServices(filters);
                        response = _documentGenerator.Generate(
                            newServicesData,
                            new[] { ChartConstans.YEAR, ChartConstans.COMMERCIAL, ChartConstans.EDUCATION, ChartConstans.HEALTH, ChartConstans.PRODUCTIVE },
                            fileFormat
                        );
                        break;
                    case SocialChartType.CUSTOMER_SATISFACTION:
                        var customerSatisfactionData = await _repository.GetCustomerSatisfaction(filters);
                        response = _documentGenerator.Generate(
                            customerSatisfactionData,
                            new[] { ChartConstans.VERY_SATISFIED, ChartConstans.SOMEHOW_SATISFIED, ChartConstans.NEITHER_SATISFIED, ChartConstans.SOMEHOW_UNSATISFIED, ChartConstans.VERY_UNSATISFIED },
                            fileFormat
                        );
                        break;
                }
            }

            return response;
        }

        public async Task<byte[]> GetTechnical(FilterParametersViewModel filters, TechnicalChartType chartType, FileFormat format)
        {
            byte[] response = null;
            var fileFormat = _documentGenerator.GetFileFormat(format);

            using (_repository)
            {
                switch (chartType)
                {
                    case TechnicalChartType.INSTALLED_CAPACITY:
                        var installedCapacityData = await _repository.GetInstalledCapacity(filters);
                        response = _documentGenerator.Generate(
                            installedCapacityData,
                            new[] { ChartConstans.YEAR, ChartConstans.PV, ChartConstans.HYDRO, ChartConstans.BIOMASS, ChartConstans.WIND, ChartConstans.CONVENTIONAL },
                            fileFormat
                        );
                        break;
                    case TechnicalChartType.AVERAGE_CONSUMPTIONS:
                        var employmentsData = await _repository.GetAverageConsumption(filters);
                        response = _documentGenerator.Generate(
                            employmentsData,
                            new[] { ChartConstans.MONTH, ChartConstans.TOTAL, ChartConstans.PRODUCTIVE, ChartConstans.RESIDENTIAL, ChartConstans.COMMERCIAL, ChartConstans.PUBLIC },
                            fileFormat
                        );
                        break;
                    case TechnicalChartType.CAPACITY_UTILIZATION:
                        var capacityUtilizationData = await _repository.GetCapacityUtilization(filters);
                        response = _documentGenerator.Generate(
                            capacityUtilizationData,
                            new[] { ChartConstans.DATE, ChartConstans.VALUE },
                            fileFormat
                        );
                        break;
                    case TechnicalChartType.ELECTRICITY_CONSUMPTION:
                        var electricityConsumptionData = await _repository.GetEletricityConsumption(filters);
                        response = _documentGenerator.Generate(
                            electricityConsumptionData,
                            new[] { ChartConstans.YEAR, ChartConstans.TOTAL, ChartConstans.PRODUCTIVE, ChartConstans.COMMERCIAL, ChartConstans.RESIDENTIAL, ChartConstans.PUBLIC },
                            fileFormat
                        );
                        break;
                }
            }

            return response;
        }

        public async Task<byte[]> GetFinancial(FilterParametersViewModel filters, FinancialChartType chartType, FileFormat format)
        {
            byte[] response = null;
            var fileFormat = _documentGenerator.GetFileFormat(format);

            using (_repository)
            {
                var previousYear = DateTime.UtcNow.Year - 1;
                switch (chartType)
                {
                    case FinancialChartType.REVENUE:
                        var revenuData = await _repository.GetRevenues(filters);
                        response = _documentGenerator.Generate(
                            revenuData,
                            new[] { ChartConstans.YEAR, ChartConstans.TOTAL, ChartConstans.COMMERCIAL, ChartConstans.PRODUCTIVE, ChartConstans.RESIDENTIAL, ChartConstans.PUBLIC },
                            fileFormat
                        );
                        break;
                    case FinancialChartType.CAPEX:
                        var capexData = await _repository.GetCapex(filters);
                        response = _documentGenerator.Generate(
                            capexData,
                            new[] {
                                ChartConstans.GENERATION, ChartConstans.SITE_DEVELOPMENT, ChartConstans.LOGISTICS,
                                ChartConstans.DISTRIBUTIONS, ChartConstans.COMMISSIONING, ChartConstans.TAXES, ChartConstans.CUSTOMER_INSTALLATION
                            },
                            fileFormat
                        );
                        break;
                    case FinancialChartType.OPEX:
                        var opexData = await _repository.GetOpex(filters);
                        response = _documentGenerator.Generate(
                            opexData,
                            new[] { ChartConstans.SITE_SPECIFIC, ChartConstans.COMPANY_LEVEL, ChartConstans.TAXES, ChartConstans.LOAN_REPAYMENTS },
                            fileFormat
                        );
                        break;
                    case FinancialChartType.FINANCING_STRUCTURE:
                        var financingStructure = await _repository.GetFinance(filters);
                        response = _documentGenerator.Generate(
                            financingStructure,
                            new[] { ChartConstans.DEBT, ChartConstans.EQUITY, ChartConstans.GRANT },
                            fileFormat
                        );
                        break;
                }
            }

            return response;
        }
    }
}
