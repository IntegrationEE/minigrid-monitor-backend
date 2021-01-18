using System.Threading.Tasks;
using System.Collections.Generic;
using Monitor.Common.Models;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public interface IUserService
    {
        Task<IList<UserViewModel>> GetAll(UserInfo currentUser);
        Task<UserViewModel> Get(int id);
        Task<UserDetailsModel> GetCurrent(int id);
        Task<UserViewModel> Create(UserViewModel model);
        Task<UserViewModel> Update(int id, UserViewModel model);
        Task<UserDetailsModel> UpdateCurrent(int id, UserDetailsModel model);
        Task ChangePassword(int id, PasswordModel model);
        Task Approve(int id, ApproveModel model);
        Task ToggleIsHeadOfCompany(int id);
        Task Delete(int id);
    }
}
