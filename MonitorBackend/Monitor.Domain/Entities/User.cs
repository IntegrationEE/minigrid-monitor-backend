using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Monitor.Domain.Base;
using Monitor.Common.Enums;

namespace Monitor.Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            Programmes = new HashSet<UserProgramme>();
        }

        [Required]
        [StringLength(50)]
        public string Login { get; private set; }

        public string Password { get; private set; }
        public string PasswordToken { get; private set; }

        [Required]
        [StringLength(50)]
        public string Email { get; private set; }

        public string EmailToken { get; private set; }

        [StringLength(200)]
        public string FullName { get; private set; }
        public RoleCode Role { get; private set; }

        public int? CompanyId { get; private set; }
        public virtual Company Company { get; private set; }

        public bool IsActive { get; private set; }
        public bool IsHeadOfCompany { get; private set; }

        public virtual ICollection<UserProgramme> Programmes { get; set; }

        public void Set(string login, string email, RoleCode role)
        {
            Login = login;
            Email = email;
            Role = role;
        }

        public void SetCompany(Company company)
        {
            Company = company;
        }

        public void SetCompanyId(int companyId)
        {
            CompanyId = companyId;
        }

        public void SetFullName(string fullName)
        {
            FullName = fullName;
        }

        public void ToggleIsActive()
        {
            IsActive = true;
        }

        public void SetNewPassword(string password)
        {
            Password = password;
            PasswordToken = null;
        }

        public void SetPasswordToken(string token)
        {
            PasswordToken = token;
        }

        public void SetEmailToken(string token)
        {
            EmailToken = token;
        }

        public void SetIsHeadOfCompany(bool isHead)
        {
            IsHeadOfCompany = isHead;
        }
    }
}
