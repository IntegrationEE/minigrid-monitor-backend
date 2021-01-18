using System;
using AutoMapper;
using System.Linq;
using Monitor.Common;
using Monitor.Domain.Base;
using Monitor.Common.Enums;
using Monitor.Domain.Entities;
using Monitor.Common.Extensions;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;

namespace Monitor.Domain
{
    public class GlobalProfile : Profile
    {
        public GlobalProfile()
        {
            // Define mapping only for Entity -> ViewModel
            #region Chart Configuration
            CreateMap<ChartConfiguration, ChartConfigurationViewModel>();
            #endregion

            #region Company
            CreateMap<Company, CompanyViewModel>()
                .ForMember(x => x.StateName, opt => opt.MapFrom(x => x.State.Name))
                .ForMember(x => x.LocalGovernmentAreaName, opt => opt.MapFrom(x => x.LocalGovernmentArea.Name));
            CreateMap<Company, BaseLightModel>();
            CreateMap<Company, FilterLightModel>()
                .ForMember(x => x.SiteIds, opt => opt.MapFrom(x => x.Sites.Select(z => z.Id)));
            #endregion

            #region Consumption
            CreateMap<Consumption, ConsumptionViewModel>()
                .ForMember(z => z.Commercial, opt => opt.MapFrom(z => z.Commercial.DividingByThousand()))
                .ForMember(z => z.Residential, opt => opt.MapFrom(z => z.Residential.DividingByThousand()))
                .ForMember(z => z.Productive, opt => opt.MapFrom(z => z.Productive.DividingByThousand()))
                .ForMember(z => z.Public, opt => opt.MapFrom(z => z.Public.DividingByThousand()))
                .ForMember(z => z.PeakLoad, opt => opt.MapFrom(z => z.PeakLoad.DividingByThousand()))
                .ForMember(z => z.Total, opt => opt.MapFrom(z => z.Total.DividingByThousand()));
            #endregion

            #region Employment
            CreateMap<Employment, EmploymentViewModel>();
            #endregion

            #region Finance Capex
            CreateMap<FinanceCapex, FinanceCapexViewModel>();
            #endregion

            #region Finance Opex
            CreateMap<FinanceOpex, FinanceOpexViewModel>();
            #endregion

            #region Integrations
            CreateMap<Integration, IntegrationLightModel>();
            CreateMap<Integration, IntegrationViewModel>()
                .ForMember(z => z.Steps, opt => opt.MapFrom(z => z.Steps
                    .Where(z => !z.IsArchive)
                    .OrderBy(x => x.Ordinal)
                    .Select(x => new IntegrationStepViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Ordinal = x.Ordinal,
                        Function = x.Function,
                    }))
                );
            #endregion

            #region Integration Records
            CreateMap<IntegrationRecord, IntegrationRecordLightModel>()
                .ForMember(z => z.StatusCode, opt => opt.MapFrom(z => z.Status))
                .ForMember(z => z.StepName, opt => opt.MapFrom(z => z.StepId.HasValue ? (z.Step.Name + (z.Step.IsArchive ? " (Archive)" : string.Empty)) : null));
            #endregion

            #region LGA
            CreateMap<LocalGovernmentArea, BaseLightModel>();
            CreateMap<LocalGovernmentArea, LocalGovernmentAreaViewModel>()
                .ForMember(x => x.StateName, opt => opt.MapFrom(x => x.State.Name));
            #endregion

            #region Metering Type
            CreateMap<MeteringType, MeteringTypeViewModel>();
            #endregion

            #region New Service
            CreateMap<NewService, NewServiceViewModel>();
            #endregion

            #region People Connected
            CreateMap<PeopleConnected, PeopleConnectedViewModel>();
            #endregion

            #region Programme
            CreateMap<Programme, ProgrammeViewModel>();
            CreateMap<Programme, FilterLightModel>()
                .ForMember(x => x.SiteIds, opt => opt.MapFrom(x => x.Sites.Select(z => z.Id)));
            #endregion

            #region Programme's Indicator
            CreateMap<ProgrammeIndicator, ProgrammeIndicatorViewModel>();
            CreateMap<ProgrammeIndicator, ProgrammeIndicatorLightModel>();

            CreateMap<ProgrammeIndicatorValue, ProgrammeIndicatorValueViewModel>();
            #endregion

            #region Revenue
            CreateMap<Revenue, RevenueViewModel>();
            #endregion

            #region Settings
            CreateMap<Setting, SettingViewModel>();
            #endregion

            #region Site
            CreateMap<Site, SiteLightModel>()
                .ForMember(x => x.RenewableCapacity, opt => opt.MapFrom(x =>
                    x.TechnicalParameter != null ? (x.TechnicalParameter.RenewableCapacity / Constants.THOUSAND).Round(0) : 0
                ))
                .ForMember(x => x.RenewableTechnology, opt => opt.MapFrom(x =>
                    x.TechnicalParameter != null ? (RenewableTechnology?)x.TechnicalParameter.RenewableTechnology : null
                ))
                .ForMember(x => x.GridConnection, opt => opt.MapFrom(x =>
                    x.TechnicalParameter != null ? (GridConnectionType?)x.TechnicalParameter.GridConnection : null
                ));

            CreateMap<Site, SiteDashboardModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.State, opt => opt.MapFrom(x => x.State.Name))
                .ForMember(x => x.Company, opt => opt.MapFrom(x => x.Company.Name))
                .ForMember(x => x.Programme, opt => opt.MapFrom(x => x.Programme.Name))
                .ForMember(x => x.RenewableCapacity, opt => opt.MapFrom(x =>
                    x.TechnicalParameter != null ? (x.TechnicalParameter.RenewableCapacity / Constants.THOUSAND).Round(2) : 0
                ));

            CreateMap<Site, SiteViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.StateName, opt => opt.MapFrom(x => x.State.Name))
                .ForMember(x => x.CompanyName, opt => opt.MapFrom(x => x.Company.Name))
                .ForMember(x => x.ProgrammeName, opt => opt.MapFrom(x => x.Programme.Name));

            CreateMap<Site, SiteCardModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
                .ForMember(x => x.StateName, opt => opt.MapFrom(x => x.State.Name))
                .ForMember(x => x.ConventionalTechnology, opt => opt.MapFrom(x =>
                    x.TechnicalParameter != null ? x.TechnicalParameter.ConventionalTechnology : null
                ))
                .ForMember(x => x.RenewableTechnology, opt => opt.MapFrom(x =>
                    x.TechnicalParameter != null ? (RenewableTechnology?)x.TechnicalParameter.RenewableTechnology : null
                ))
                .ForMember(x => x.RenewableCapacity, opt => opt.MapFrom(x =>
                    x.TechnicalParameter != null ? (x.TechnicalParameter.RenewableCapacity / Constants.THOUSAND).Round(2) : 0
                ))
                .ForMember(x => x.GridConnection, opt => opt.MapFrom(x =>
                    x.TechnicalParameter != null ? x.TechnicalParameter.GridConnection : GridConnectionType.OFF_GRID
                ))
                .ForMember(x => x.Status, opt => opt.MapFrom(x =>
                    x.Modified.HasValue && x.Modified.Value < DateTime.UtcNow.AddMonths(-6) ?
                        SiteStatus.OUT_OF_OPERATION :
                        (
                            x.TechnicalParameter == null || x.FinanceCapex == null ?
                                SiteStatus.INFORMATION_MISSING : SiteStatus.UP_TO_DATE
                        )
                ));
            #endregion

            #region Site Technical Parameter
            CreateMap<SiteTechParameter, SiteTechParameterViewModel>()
                .ForMember(z => z.ConventionalCapacity, opt => opt.MapFrom(z => z.ConventionalCapacity / Constants.THOUSAND))
                .ForMember(z => z.RenewableCapacity, opt => opt.MapFrom(z => z.RenewableCapacity / Constants.THOUSAND))
                .ForMember(z => z.StorageCapacity, opt => opt.MapFrom(z => z.StorageCapacity / Constants.THOUSAND));
            #endregion

            #region State
            CreateMap<State, BaseLightModel>();
            CreateMap<State, FilterLightModel>()
                .ForMember(x => x.SiteIds, opt => opt.MapFrom(x => x.Sites.Select(z => z.Id)));
            #endregion

            #region Tariff
            CreateMap<Tariff, TariffViewModel>();
            #endregion

            #region Threshold
            CreateMap<Threshold, ThresholdViewModel>();
            #endregion

            #region User
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.CompanyName, opt => opt.MapFrom(x => x.CompanyId.HasValue ? x.Company.Name : null))
                .ForMember(z => z.Programmes, opt => opt.MapFrom(z => z.Programmes.Select(x => x.ProgrammeId)));
            CreateMap<User, UserDetailsModel>()
                .ForMember(x => x.City, opt => opt.MapFrom(x => x.CompanyId.HasValue ? x.Company.City : null))
                .ForMember(x => x.Company, opt => opt.MapFrom(x => x.CompanyId.HasValue ? x.Company.Name : null))
                .ForMember(x => x.LocalGovernmentAreaId, opt => opt.MapFrom(x => x.CompanyId.HasValue ? (int?)x.Company.LocalGovernmentAreaId : null))
                .ForMember(x => x.LocalGovernmentArea, opt => opt.MapFrom(x => x.CompanyId.HasValue ? x.Company.LocalGovernmentArea.Name : null))
                .ForMember(x => x.Number, opt => opt.MapFrom(x => x.CompanyId.HasValue ? x.Company.Number : null))
                .ForMember(x => x.State, opt => opt.MapFrom(x => x.CompanyId.HasValue ? x.Company.State.Name : null))
                .ForMember(x => x.StateId, opt => opt.MapFrom(x => x.CompanyId.HasValue ? (int?)x.Company.StateId : null))
                .ForMember(x => x.Street, opt => opt.MapFrom(x => x.CompanyId.HasValue ? x.Company.Street : null))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(x => x.CompanyId.HasValue ? x.Company.PhoneNumber : null));
            #endregion
        }
    }
}
