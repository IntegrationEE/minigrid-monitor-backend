using System.Threading.Tasks;
using Monitor.Common.Models;

namespace Monitor.Business.Services
{
    public interface IAccountService
    {
        Task Register(SignUpModel model);
        Task SendRegisterationEmail(SignUpModel model);
        Task ConfirmEmail(ConfirmEmailModel model);
        Task ForgotPassword(ForgotPasswordModel model);
        Task ResetPassword(PasswordResetModel model);
        Task SetDetails(int userId, FirstSignInModel model);
    }
}
