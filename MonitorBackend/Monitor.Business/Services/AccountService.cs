using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Infrastructure;
using Monitor.Common.Helpers;
using Monitor.Domain.Entities;
using Monitor.Business.Repositories;

namespace Monitor.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository _repository;
        private readonly IMailRepository _mailRepository;

        public AccountService(IBaseRepository repository, IMailRepository mailRepository)
        {
            _repository = repository;
            _mailRepository = mailRepository;
        }

        public async Task Register(SignUpModel model)
        {
            using (_repository)
            {
                model.IsValid();

                if (await _repository.Exists<User>(x => x.Email == model.Email))
                { throw new CustomException("Error: Your email address is already registered. Please use another one or contact the administrator"); }
                else if (await _repository.Exists<User>(x => x.Login == model.Username))
                { throw new CustomException("Error: Your username is already registered. Please use another one or contact the administrator"); }

                var entity = new User();
                entity.Set(model.Username, model.Email, RoleCode.GUEST);
                entity.SetNewPassword(model.Password);
                entity.SetEmailToken(TokenHelper.Generate());

                await _repository.Add(entity);
                await _repository.SaveChanges();

                await _mailRepository.SendMessage(MailType.REGISTRATION, model.Email, $"{model.BaseUrl}/{entity.EmailToken}", model.Username);
            }
        }

        public async Task SendRegisterationEmail(SignUpModel model)
        {
            using (_repository)
            {
                var entity = await _repository.GetQuery<User>(x => x.Login == model.Username, true)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException($"User with Name: '{model.Username}' doesn't exist."); }

                entity.SetEmailToken(TokenHelper.Generate());

                await _repository.SaveChanges();

                await _mailRepository.SendMessage(MailType.REGISTRATION, model.Email, $"{model.BaseUrl}/{entity.EmailToken}", model.Username);
            }
        }

        public async Task ForgotPassword(ForgotPasswordModel model)
        {
            using (_repository)
            {
                model.IsValid();

                var user = await _repository.GetQuery<User>(u => u.Email == model.Email, true)
                    .SingleOrDefaultAsync();

                if (user == null)
                { throw new CustomException($"User with provided {nameof(model.Email)} doesn't exist."); }

                user.SetPasswordToken(TokenHelper.Generate());

                await _repository.SaveChanges();

                await _mailRepository.SendMessage(MailType.RESET_PASSWORD, model.Email, $"{model.BaseUrl}/{user.PasswordToken}", null);
            }
        }

        public async Task ConfirmEmail(ConfirmEmailModel model)
        {
            model.IsValid();

            using (_repository)
            {
                var entity = await _repository.GetQuery<User>(u => u.EmailToken == model.EmailToken, true)
                    .SingleOrDefaultAsync();

                if (entity == null)
                { throw new CustomException($"User with provided {nameof(model.EmailToken)} doesn't exist."); }

                if (!entity.IsActive)
                {
                    entity.ToggleIsActive();
                    entity.SetEmailToken(null);

                    await _repository.SaveChanges();
                }
            }
        }

        public async Task ResetPassword(PasswordResetModel model)
        {
            using (_repository)
            {
                model.IsValid();

                var entity = await _repository.GetQuery<User>(i => i.PasswordToken == model.PasswordToken, true)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException($"User with provided {nameof(model.PasswordToken)} doesn't exist."); }

                entity.SetNewPassword(model.EncryptedPassword);

                await _repository.SaveChanges();
            }
        }

        public async Task SetDetails(int userId, FirstSignInModel model)
        {
            using (_repository)
            {
                await model.IsValid();

                var entity = await _repository.GetQuery<User>(x => x.Id == userId)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException("Account doesn't exist"); }

                entity.SetFullName(model.FullName);

                if (!model.CompanyId.HasValue)
                {
                    var company = new Company();
                    company.Set(model.CompanyName, model.WebsiteUrl, model.PhoneNumber);
                    company.SetAddress(model.City, model.Street, model.Number, model.StateId.Value, model.LocalGovernmentAreaId.Value);

                    await _repository.Add(company);

                    entity.SetCompany(company);
                }
                else
                {
                    entity.SetCompanyId(model.CompanyId.Value);
                }

                _repository.Update(entity);
                await _repository.SaveChanges();

                Expression<Func<User, bool>> exp = null;
                if (!await _repository.Exists<User>(z => z.CompanyId == entity.CompanyId && z.Id != userId && z.Role == RoleCode.DEVELOPER))
                {
                    exp = x => x.Id != userId && x.Role == RoleCode.ADMINISTRATOR;
                }
                else
                {
                    exp = x => x.Id != userId && x.CompanyId == entity.CompanyId && x.IsHeadOfCompany;
                }

                var recipients = await _repository.GetQuery(exp)
                     .Select(x => x.Email)
                     .ToArrayAsync();

                if (recipients.Length > 0)
                {
                    await _mailRepository.SendMessage(MailType.NEW_ACCOUNT_IN_COMPANY, recipients, username: entity.FullName);
                }
            }
        }
    }
}
