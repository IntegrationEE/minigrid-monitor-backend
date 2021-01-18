using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Monitor.Common.Models;
using Monitor.Business.Services;

namespace Monitor.WebApi.Controllers
{
    /// <summary>
    /// Accounts Controller
    /// </summary>
    [AllowAnonymous]
    public class AccountsController : AuthorizeController
    {
        private readonly IAccountService _accountService;
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="accountService"></param>
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// Create Request Account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task CreateRequestAccount([FromBody] SignUpModel model)
           => await _accountService.Register(model);
        /// <summary>
        /// Send again registration email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("resendEmail")]
        public async Task ResendRegistrationEmail([FromBody] SignUpModel model)
           => await _accountService.SendRegisterationEmail(model);
        /// <summary>
        /// Confirm Email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("confirm")]
        public async Task ConfirmEmail([FromBody] ConfirmEmailModel model)
            => await _accountService.ConfirmEmail(model);
        /// <summary>
        /// Send email with link to reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("forgotPassword")]
        public async Task ForgotPassword([FromBody] ForgotPasswordModel model)
            => await _accountService.ForgotPassword(model);
        /// <summary>
        /// Set or Reset Account Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("resetPassword")]
        public async Task SetAccountPassword([FromBody] PasswordResetModel model)
           => await _accountService.ResetPassword(model);
        /// <summary>
        /// Set account details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("details")]
        public async Task SetDetails([FromBody] FirstSignInModel model)
            => await _accountService.SetDetails(CurrentUserId, model);
    }
}