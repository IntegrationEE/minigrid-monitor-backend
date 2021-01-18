using System.Collections.Generic;
using Monitor.Common;
using Monitor.Domain.Base;
using Monitor.Common.Enums;
using Monitor.Common.Extensions;

namespace Monitor.Domain.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public UserViewModel()
        {
            Programmes = new List<int>();
        }

        public string BaseUrl { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public RoleCode Role { get; set; }
        public string RoleName => Role.GetDescription();
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public IEnumerable<int> Programmes { get; set; }
        public bool IsHeadOfCompany { get; set; }

        public void IsValid()
        {
            if (string.IsNullOrWhiteSpace(Login))
            { throw new CustomException($"{nameof(Login)} is required."); }
            else if (string.IsNullOrWhiteSpace(Email))
            { throw new CustomException($"{nameof(Email)} is required."); }
            else if (string.IsNullOrWhiteSpace(FullName))
            { throw new CustomException($"{nameof(FullName)} is required."); }
            else if (!CompanyId.HasValue)
            { throw new CustomException($"Company is required."); }
        }
    }
}
