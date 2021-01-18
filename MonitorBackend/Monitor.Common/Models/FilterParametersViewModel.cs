using System.Collections.Generic;
using Monitor.Common.Enums;

namespace Monitor.Common.Models
{
    public class FilterParametersViewModel
    {
        private decimal _from;
        private decimal _to;

        public FilterParametersViewModel()
        {
            States = new List<int>();
            Programmes = new List<int>();
            Developers = new List<int>();
            Technologies = new List<RenewableTechnology>();
            GridConnections = new List<GridConnectionType>();
        }

        public List<int> States { get; set; }

        public List<int> Programmes { get; set; }

        public List<int> Developers { get; set; }

        public List<RenewableTechnology> Technologies { get; set; }

        public List<GridConnectionType> GridConnections { get; set; }

        public int? SiteId { get; set; }

        public decimal From { get => _from; set { _from = value * Constants.THOUSAND; } }

        public decimal To { get => _to; set { _to = value * Constants.THOUSAND; } }

        public DataLevel? Level { get; set; }
    }
}