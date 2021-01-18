using System.ComponentModel.DataAnnotations;
using Monitor.Domain.Base;
using Monitor.Common.Enums;

namespace Monitor.Domain.Entities
{
    public class Setting : IEntity
    {
        public Setting() { }
        public Setting(SettingCode code, string value)
            : base()
        {
            Code = code;
            Set(value);
        }

        public SettingCode Code { get; private set; }

        [Required]
        public string Value { get; private set; }

        public int GetIntValue() => int.Parse(Value);
        public bool GetBoolValue() => bool.Parse(Value);

        public void Set(string value)
        {
            Value = value;
        }
    }
}
