using Microsoft.Extensions.DependencyInjection;
using Monitor.HostedServices;
using Monitor.Infrastructure;
using Monitor.Business.Services;
using Monitor.Business.Repositories;

namespace Monitor.WebApi
{
    /// <summary>
    /// Service Collection Extension
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Add Services and Repositories
        /// </summary>
        /// <param name="services"></param>
        public static void AddServicesAndRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAdvancedAnalyticsRepository, AdvancedAnalyticsRepository>();
            services.AddScoped<IAdvancedTechnicalService, AdvancedTechnicalService>();
            services.AddScoped<IAdvancedFinancialService, AdvancedFinancialService>();
            services.AddScoped<IAdvancedSocialService, AdvancedSocialService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IBaseRepository, BaseRepository>();

            services.AddScoped<IChartConfigurationRepository, ChartConfigurationRepository>();
            services.AddScoped<IChartConfigurationService, ChartConfigurationService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IConsumptionService, ConsumptionService>();
            services.AddScoped<ICustomerSatisfactionService, CustomerSatisfactionService>();

            services.AddScoped<IEmploymentService, EmploymentService>();
            services.AddScoped<IEnumService, EnumService>();

            services.AddScoped<IFinanceCapexService, FinanceCapexService>();
            services.AddScoped<IFileGeneratorService, FileGeneratorService>();
            services.AddScoped<IFinanceOpexService, FinanceOpexService>();

            services.AddScoped<IGraphService, GraphService>();

            services.AddScoped<IIntegrationRecordService, IntegrationRecordService>();
            services.AddScoped<IIntegrationService, IntegrationService>();
            services.AddSingleton<IIntegrationHostedService, IntegrationHostedService>();

            services.AddScoped<ILocalGovernmentAreaService, LocalGovernmentAreaService>();

            services.AddScoped<IMailRepository, MailRepository>();
            services.AddScoped<IMeteringTypeService, MeteringTypeService>();

            services.AddScoped<INewServiceService, NewServiceService>();

            services.AddScoped<IPeopleConnectedService, PeopleConnectedService>();
            services.AddScoped<IProgrammeService, ProgrammeService>();
            services.AddScoped<IProgrammeAnalyticsService, ProgrammeAnalyticsService>();
            services.AddScoped<IProgrammeIndicatorService, ProgrammeIndicatorService>();
            services.AddScoped<IProgrammeIndicatorValueService, ProgrammeIndicatorValueService>();
            services.AddScoped<IPortfolioService, PortfolioService>();

            services.AddScoped<IRevenueService, RevenueService>();

            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<ISiteService, SiteService>();
            services.AddScoped<ISiteTechnicalParameterService, SiteTechnicalParameterService>();
            services.AddScoped<ISiteStatusService, SiteStatusService>();
            services.AddScoped<ISiteQrService, SiteQrService>();
            services.AddScoped<IStateService, StateService>();

            services.AddScoped<ITariffService, TariffService>();
            services.AddScoped<IThresholdService, ThresholdService>();

            services.AddScoped<IUserService, UserService>();
        }
    }
}
