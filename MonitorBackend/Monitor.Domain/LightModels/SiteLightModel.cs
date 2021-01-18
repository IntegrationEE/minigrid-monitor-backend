using Monitor.Domain.Base;
using Monitor.Common.Enums;

namespace Monitor.Domain.LightModels
{
    public class SiteLightModel : BaseLightModel
    {
        public decimal RenewableCapacity { get; set; }
        public RenewableTechnology? RenewableTechnology { get; set; }
        public GridConnectionType? GridConnection { get; set; }
    }
}
