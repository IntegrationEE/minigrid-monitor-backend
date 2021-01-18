using Monitor.Common.Enums;

namespace Monitor.Common.Models
{
    public class SignInModel
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public RoleCode Role { get; set; }

        public int? CompanyId { get; set; }

        public bool IsAnonymous { get; set; }

        public bool IsHeadOfCompany { get; set; }
    }
}
