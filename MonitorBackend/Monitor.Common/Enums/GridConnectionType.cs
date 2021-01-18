using System.ComponentModel;

namespace Monitor.Common.Enums
{
    public enum GridConnectionType
    {
        [Description("Interconnected")]
        ON_GRID = 1,

        [Description("Isolated")]
        OFF_GRID,
    }
}
