using Monitor.Common.Enums;

namespace Monitor.Business.Common
{
    public class GraphModel
    {
        public string Name { get; set; }
        public string StateName { get; set; }
        public string ProgrammeName { get; set; }
        public string CompanyName { get; set; }
        public RenewableTechnology? RenewableTechnology { get; set; }
        public ConventionalTechnology? ConventionalTechnology { get; set; }
        public StorageTechnology? StorageTechnology { get; set; }
        public GridConnectionType GridConnection { get; set; }

        public decimal RenewableCapacity { get; set; }
        public decimal? ConventionalCapacity { get; set; }
        public decimal? StorageCapacity { get; set; }
        public decimal? GridLength { get; set; }

        public int CommercialConnections { get; set; }
        public int ResidentialConnections { get; set; }
        public int ProductiveConnections { get; set; }
        public int PublicConnections { get; set; }

        public decimal CommercialTariffs { get; set; }
        public decimal ResidentialTariffs { get; set; }
        public decimal ProductiveTariffs { get; set; }
        public decimal PublicTariffs { get; set; }
    }
}
