using System.Linq;
using System.Collections.Generic;
using Monitor.Common.Enums;
using Monitor.Domain.Entities;

namespace Monitor.Domain.Extensions
{
    public static class SettingsExtension
    {
        public static Setting Get(this ICollection<Setting> settings, SettingCode code) => settings.First(x => x.Code == code);
    }
}
