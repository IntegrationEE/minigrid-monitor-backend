using Monitor.Common;
using Monitor.Domain.Base;
using Monitor.Common.Enums;

namespace Monitor.Domain.ViewModels
{
    public class SiteTechParameterViewModel : BaseViewModel
    {
        public int SiteId { get; set; }

        public RenewableTechnology RenewableTechnology { get; set; }
        public decimal RenewableCapacity { get; set; }

        public GridConnectionType GridConnection { get; set; }
        public decimal? GridLength { get; set; }

        public StorageTechnology? StorageTechnology { get; set; }
        public decimal StorageCapacity { get; set; }


        public ConventionalTechnology? ConventionalTechnology { get; set; }
        public decimal ConventionalCapacity { get; set; }


        public int MeteringTypeId { get; set; }
        public string MeterManufacturer { get; set; }
        public string InverterManufacturer { get; set; }

        public void IsValid()
        {
            if (RenewableCapacity <= 0)
            { throw new CustomException("Renewable Capacity should be greater than 0"); }
            else if (StorageTechnology.HasValue && StorageCapacity <= 0)
            { throw new CustomException("Storage Capacity should be greater than 0"); }
            else if (ConventionalTechnology.HasValue && ConventionalCapacity <= 0)
            { throw new CustomException("Conventional Capacity should be greater than 0"); }
        }
    }
}
