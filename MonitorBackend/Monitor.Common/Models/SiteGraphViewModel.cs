using Monitor.Common.Enums;
using System.Collections.Generic;

namespace Monitor.Common.Models
{
    public class SiteGraphViewModel
    {
        public SiteGraphViewModel()
        {
            Nodes = new List<GraphNodeViewModel>();
        }

        public RenewableTechnology RenewableTechnology { get; set; }

        public ConventionalTechnology? ConventionalTechnology { get; set; }

        public StorageTechnology? StorageTechnology { get; set; }

        public GridConnectionType GridConnection { get; set; }

        public List<SiteGraphLabelViewModel> Details { get; set; }

        public List<GraphNodeViewModel> Nodes { get; set; }
    }

    public class SiteGraphLabelViewModel
    {
        public string Title { get; set; }

        public string Value { get; set; }
    }

    public class GraphNodeViewModel
    {
        public GraphNodeViewModel()
        {
            Connections = new List<GraphNode>();
        }

        public GraphNode Index { get; set; }

        public string Unit { get; set; }

        public decimal? Value { get; set; }

        public string Title { get; set; }

        public List<GraphNode> Connections { get; set; }
    }
}
