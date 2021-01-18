using System.ComponentModel;

namespace Monitor.Common.Enums
{
    public enum IntegrationStatusCode
    {
        [Description("Run created")]
        RUN_CREATED = 1,

        [Description("Run finished successfully")]
        RUN_COMPLETED,

        [Description("Step finished successfully")]
        STEP_COMPLETED,

        [Description("Step canceled with error")]
        STEP_CANCELED,
    }
}
