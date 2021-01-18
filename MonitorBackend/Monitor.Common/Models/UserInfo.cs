using Monitor.Common.Enums;

namespace Monitor.Common.Models
{
    public class UserInfo
    {
        public int Id { get; set; }

        public int? CompanyId { get; set; }

        public RoleCode Role { get; set; }

        public bool IsAnonymuos { get; set; }
    }
}
