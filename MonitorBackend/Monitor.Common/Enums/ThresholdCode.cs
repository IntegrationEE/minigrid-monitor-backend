using System.ComponentModel;

namespace Monitor.Common.Enums
{
    public enum ThresholdCode
    {
        [Description("Grid Length")]
        GRID_LENGTH = 1,

        [Description("Renewable capacity")]
        RENEWABLE_CAPACITY,

        [Description("Conventional capacity")]
        CONVENTIONAL_CAPACITY,

        [Description("Storage capacity")]
        STORAGE_CAPACITY,
    }
}
