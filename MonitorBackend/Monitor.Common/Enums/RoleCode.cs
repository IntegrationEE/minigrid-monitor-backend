using System.ComponentModel;

namespace Monitor.Common.Enums
{
    public enum RoleCode
    {
        [Description("Guest")]
        GUEST = 1,

        [Description("Programme Manager")]
        PROGRAMME_MANAGER,

        [Description("Developer")]
        DEVELOPER,

        [Description("Administrator")]
        ADMINISTRATOR
    }
}
