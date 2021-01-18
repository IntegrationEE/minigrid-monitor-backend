using Monitor.Common;
using Monitor.Domain.Base;
using Monitor.Common.Enums;

namespace Monitor.Domain.Entities
{
    public class SiteTechParameter : BaseEntity
    {
        public SiteTechParameter() { }
        public SiteTechParameter(int siteId)
            : base()
        {
            SiteId = siteId;
        }

        public int SiteId { get; private set; }

        public virtual Site Site { get; set; }

        public RenewableTechnology RenewableTechnology { get; private set; }
        public decimal RenewableCapacity { get; private set; }

        public GridConnectionType GridConnection { get; private set; }
        public decimal? GridLength { get; private set; }

        public StorageTechnology? StorageTechnology { get; private set; }
        public decimal StorageCapacity { get; private set; }

        public ConventionalTechnology? ConventionalTechnology { get; private set; }
        public decimal ConventionalCapacity { get; private set; }

        public int MeteringTypeId { get; private set; }
        public virtual MeteringType MeteringType { get; private set; }
        public string MeterManufacturer { get; private set; }
        public string InverterManufacturer { get; private set; }

        public void SetRenewableCapacity(RenewableTechnology technology, decimal capacity)
        {
            RenewableTechnology = technology;
            RenewableCapacity = capacity * Constants.THOUSAND;
        }

        public void SetManufacturer(string inverter, string meters, int meteringTypeId)
        {
            InverterManufacturer = inverter;
            MeterManufacturer = meters;
            MeteringTypeId = meteringTypeId;
        }

        public void SetConventionalCapacity(ConventionalTechnology? technology, decimal capacity)
        {
            ConventionalTechnology = technology;
            ConventionalCapacity = technology.HasValue ? capacity * Constants.THOUSAND : 0;
        }

        public void SetStorageCapacity(StorageTechnology? technology, decimal capacity)
        {
            StorageTechnology = technology;
            StorageCapacity = technology.HasValue ? capacity * Constants.THOUSAND : 0;
        }

        public void SetGrid(GridConnectionType gridConnection, decimal? gridLength)
        {
            GridConnection = gridConnection;
            GridLength = gridLength;
        }
    }
}
