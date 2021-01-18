using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Infrastructure;
using Monitor.Business.Common;
using Monitor.Domain.Entities;
using Monitor.Common.Extensions;
using Monitor.Business.Repositories;

namespace Monitor.Business.Services
{
    public class GraphService : IGraphService
    {
        private readonly IBaseRepository _repository;
        private readonly ISettingRepository _settingRepository;


        public GraphService(IBaseRepository repository, ISettingRepository settingRepository)
        {
            _repository = repository;
            _settingRepository = settingRepository;
        }

        public async Task<SiteGraphViewModel> Get(int id)
        {
            using (_repository)
            {
                var data = await GetData(id);
                var currency = await _settingRepository.GetCurrency();

                return GetGraph(data, currency);
            }
        }

        private async Task<GraphModel> GetData(int id)
        {
            return await _repository.GetQuery<Site>(a => a.Id == id)
                .Select(x => new GraphModel
                {
                    Name = x.Name,
                    StateName = x.State.Name,
                    ProgrammeName = x.Programme.Name,
                    CompanyName = x.Company.Name,

                    RenewableTechnology = x.TechnicalParameter != null ? (RenewableTechnology?)x.TechnicalParameter.RenewableTechnology : null,
                    RenewableCapacity = x.TechnicalParameter != null ?
                        x.TechnicalParameter.RenewableCapacity / Constants.THOUSAND : 0,

                    ConventionalTechnology = x.TechnicalParameter != null ? x.TechnicalParameter.ConventionalTechnology : null,
                    ConventionalCapacity = x.TechnicalParameter != null && x.TechnicalParameter.ConventionalTechnology.HasValue ?
                        (decimal?)x.TechnicalParameter.ConventionalCapacity / Constants.THOUSAND : null,

                    StorageTechnology = x.TechnicalParameter != null ? x.TechnicalParameter.StorageTechnology : null,
                    StorageCapacity = x.TechnicalParameter != null && x.TechnicalParameter.StorageTechnology.HasValue ?
                        (decimal?)x.TechnicalParameter.StorageCapacity / Constants.THOUSAND : null,

                    GridConnection = x.TechnicalParameter != null ? x.TechnicalParameter.GridConnection : GridConnectionType.OFF_GRID,
                    GridLength = x.TechnicalParameter != null ? x.TechnicalParameter.GridLength : 0,

                    CommercialConnections = x.PeopleConnected.Sum(s => s.Commercial),
                    ResidentialConnections = x.PeopleConnected.Sum(s => s.Residential),
                    ProductiveConnections = x.PeopleConnected.Sum(s => s.Productive),
                    PublicConnections = x.PeopleConnected.Sum(s => s.Public),

                    CommercialTariffs = x.Tariffs.Sum(s => s.Commercial),
                    ResidentialTariffs = x.Tariffs.Sum(s => s.Residential),
                    ProductiveTariffs = x.Tariffs.Sum(s => s.Productive),
                    PublicTariffs = x.Tariffs.Sum(s => s.Public),
                }).FirstOrDefaultAsync();
        }

        private SiteGraphViewModel GetGraph(GraphModel data, string currency)
        {
            var response = new SiteGraphViewModel()
            {
                RenewableTechnology = data?.RenewableTechnology ?? RenewableTechnology.PV,
                ConventionalTechnology = data?.ConventionalTechnology,
                StorageTechnology = data?.StorageTechnology,
                GridConnection = data?.GridConnection ?? GridConnectionType.OFF_GRID,
                Details = new List<SiteGraphLabelViewModel>
                {
                    new SiteGraphLabelViewModel {
                        Title = "Name",
                        Value = data?.Name
                    },
                    new SiteGraphLabelViewModel {
                        Title = "State",
                        Value = data?.StateName
                    },
                    new SiteGraphLabelViewModel {
                        Title = "Programme",
                        Value = data?.ProgrammeName
                    },
                    new SiteGraphLabelViewModel {
                        Title = "Company",
                        Value = data?.CompanyName
                    },
                    new SiteGraphLabelViewModel {
                        Title = (data?.RenewableTechnology ?? RenewableTechnology.PV).GetDescription(),
                        Value = $"{data?.RenewableCapacity.Round(0) ?? 0} {Constants.UNIT_OF_CAPACITY}"
                    },
                },
                Nodes = GetNodes(data, currency)
                    .OrderBy(z => z.Index)
                    .ToList()
            };

            return response;
        }

        private List<GraphNodeViewModel> GetNodes(GraphModel data, string currency)
        {
            return new List<GraphNodeViewModel>()
            {
                new GraphNodeViewModel
                {
                    Index = GraphNode.RENEWABLE_CAPACITY,
                    Connections = new List<GraphNode> { GraphNode.INVERTER },
                    Title = $"{(data?.RenewableTechnology ?? RenewableTechnology.PV).GetDescription()} {Constants.CAPACITY}",
                    Unit = Constants.UNIT_OF_CAPACITY,
                    Value = data?.RenewableCapacity.Round(0) ?? 0,
                },
                new GraphNodeViewModel
                {
                    Index = GraphNode.CONVENTIONAL_CAPACITY,
                    Connections = new List<GraphNode> { GraphNode.INVERTER },
                    Title = data.ConventionalTechnology.HasValue ? $"{data.ConventionalTechnology.Value.GetDescription()} {Constants.CAPACITY}" : string.Empty,
                    Unit = Constants.UNIT_OF_CAPACITY,
                    Value = data?.ConventionalCapacity?.Round(0)
                },
                new GraphNodeViewModel
                {
                    Index = GraphNode.INVERTER,
                    Connections = new List<GraphNode> { GraphNode.GRID },
                },
                new GraphNodeViewModel
                {
                    Index = GraphNode.STORAGE_CAPACITY,
                    Connections = new List<GraphNode> { GraphNode.INVERTER },
                    Title = data.StorageTechnology.HasValue ? $"{data.StorageTechnology.Value.GetDescription()} {Constants.CAPACITY}" : string.Empty,
                    Unit = Constants.UNIT_OF_BATTERY_CAPACITY,
                    Value = data?.StorageCapacity?.Round(0),
                },
                new GraphNodeViewModel
                {
                    Index = GraphNode.GRID,
                    Connections = new List<GraphNode> { GraphNode.COMMERCIAL_CONN, GraphNode.RESIDENTIAL_CONN, GraphNode.PRODUCTIVE_CONN, GraphNode.PUBLIC_CONN },
                    Title = Constants.GRID_LENGTH,
                    Unit = Constants.UNIT_OF_GRID,
                    Value = data?.GridLength?.Round(0),
                },
                // Connections
                new GraphNodeViewModel
                {
                    Index = GraphNode.COMMERCIAL_CONN,
                    Connections = new List<GraphNode> { GraphNode.COMMERCIAL_TARIFF },
                    Title = Constants.COMMERCIAL_CONN,
                    Value = data?.CommercialConnections ?? 0,
                },
                new GraphNodeViewModel
                {
                    Index = GraphNode.RESIDENTIAL_CONN,
                    Connections = new List<GraphNode> { GraphNode.RESIDENTIAL_TARIFF },
                    Title = Constants.RESIDENTIAL_CONN,
                    Value = data?.ResidentialConnections ?? 0,
                },
                new GraphNodeViewModel
                {
                    Index = GraphNode.PRODUCTIVE_CONN,
                    Connections = new List<GraphNode> { GraphNode.PRODUCTIVE_TARIFF },
                    Title = Constants.PRODUCTIVE_CONN,
                    Value = data?.ProductiveConnections ?? 0,
                },
                new GraphNodeViewModel
                {
                    Index = GraphNode.PUBLIC_CONN,
                    Connections = new List<GraphNode> { GraphNode.PUBLIC_TARIFF },
                    Title = Constants.PUBLIC_CONN,
                    Value = data?.PublicConnections ?? 0,
                },
                // Tariffs
                new GraphNodeViewModel
                {
                    Index = GraphNode.COMMERCIAL_TARIFF,
                    Title = Constants.COMMERCIAL_TARIFF,
                    Unit = $"{currency}/{Constants.UNIT_OF_BATTERY_CAPACITY}",
                    Value = data?.CommercialTariffs.Round(0),
                },
                new GraphNodeViewModel
                {
                    Index = GraphNode.RESIDENTIAL_TARIFF,
                    Title = Constants.RESIDENTIAL_TARIFF,
                    Unit = $"{currency}/{Constants.UNIT_OF_BATTERY_CAPACITY}",
                    Value = data?.ResidentialTariffs.Round(0),
                },
                new GraphNodeViewModel
                {
                    Index = GraphNode.PRODUCTIVE_TARIFF,
                    Title = Constants.PRODUCTIVE_TARIFF,
                    Unit = $"{currency}/{Constants.UNIT_OF_BATTERY_CAPACITY}",
                    Value = data?.ProductiveTariffs.Round(0),
                },
                new GraphNodeViewModel
                {
                    Index = GraphNode.PUBLIC_TARIFF,
                    Title = Constants.PUBLIC_TARIFF,
                    Unit = $"{currency}/{Constants.UNIT_OF_BATTERY_CAPACITY}",
                    Value = data?.PublicTariffs.Round(0),
                }
           };
        }
    }
}
