using System.ComponentModel;

namespace Monitor.Common.Enums
{
    public enum RenewableTechnology
    {
        [Description("PV")]
        PV = 1,

        [Description("Hydro")]
        HYDRO,

        [Description("Wind")]
        WIND,

        [Description("Biomass")]
        BIOMASS
    }
}
