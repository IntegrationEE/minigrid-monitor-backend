using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Common;
using Monitor.Common.Enums;
using Monitor.Common.Models;
using Monitor.Common.Helpers;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;
using Monitor.Domain.LightModels;
using Monitor.Business.Repositories;

namespace Monitor.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository _repository;
        private readonly IMailRepository _mailRepository;

        public UserService(IBaseRepository repository, IMailRepository mailRepository)
        {
            _repository = repository;
            _mailRepository = mailRepository;
        }

        public async Task<IList<UserViewModel>> GetAll(UserInfo currentUser)
        {
            using (_repository)
            {
                Expression<Func<User, bool>> exp = null;

                if (currentUser.Role == RoleCode.DEVELOPER)
                {
                    exp = x => x.CompanyId == currentUser.CompanyId.Value && (x.Role == RoleCode.DEVELOPER || x.Role == RoleCode.GUEST);
                }

                return await _repository.GetListWithOrder<UserViewModel, User, string>(exp, x => x.FullName);
            }
        }

        public async Task<UserDetailsModel> GetCurrent(int id)
        {
            using (_repository)
            {
                return await _repository.Get<UserDetailsModel, User>(x => x.Id == id);
            }
        }

        public async Task<UserViewModel> Get(int id)
        {
            using (_repository)
            {
                return await _repository.Get<UserViewModel, User>(x => x.Id == id);
            }
        }

        public async Task<UserViewModel> Create(UserViewModel model)
        {
            using (_repository)
            {
                model.IsValid();

                if (await _repository.Exists<User>(x => x.Email == model.Email || x.Login == model.Login))
                { throw new CustomException("Exists User with entered Email or Login."); }

                var entity = new User();
                entity.SetPasswordToken(TokenHelper.Generate());
                await MapViewModel(model, entity);

                await _repository.Add(entity);
                await _repository.SaveChanges();

                await _mailRepository.SendMessage(MailType.REGISTERED_BY_ADMIN, model.Email, $"{model.BaseUrl}/{entity.PasswordToken}", model.Login);

                return await _repository.Get<UserViewModel, User>(x => x.Id == entity.Id);
            }
        }

        public async Task<UserViewModel> Update(int id, UserViewModel model)
        {
            using (_repository)
            {
                model.IsValid();

                var entity = await _repository.GetQuery<User>(x => x.Id == id, true)
                    .Include(z => z.Programmes)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException($"User with Id '{id}' doesn't exist."); }

                if (entity.Programmes?.Count > 0)
                {
                    _repository.Delete(entity.Programmes);
                    entity.Programmes.Clear();
                }

                await MapViewModel(model, entity);

                await _repository.SaveChanges();

                return await _repository.Get<UserViewModel, User>(x => x.Id == id);
            }
        }

        public async Task<UserDetailsModel> UpdateCurrent(int id, UserDetailsModel model)
        {
            using (_repository)
            {
                model.IsValid();

                var entity = await _repository.GetQuery<User>(x => x.Id == id)
                    .Include(z => z.Company)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException($"User with Id '{id}' doesn't exist."); }

                entity.SetFullName(model.FullName);

                _repository.Update(entity);

                if (entity.Company == null)
                { throw new CustomException("Can't modify Company address data because you don't belong to the Company."); }

                entity.Company.SetAddress(model.City, model.Street, model.Number, model.StateId.Value, model.LocalGovernmentAreaId.Value);
                entity.Company.Set(entity.Company.Name, entity.Company.WebsiteUrl, model.PhoneNumber);

                _repository.Update(entity.Company);

                await _repository.SaveChanges();

                return await _repository.Get<UserDetailsModel, User>(x => x.Id == id);
            }
        }

        public async Task ChangePassword(int id, PasswordModel model)
        {
            using (_repository)
            {
                model.IsValid();

                var entity = await _repository.GetQuery<User>(x => x.Id == id, true)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException($"User with Id '{id}' doesn't exist."); }
                else if (entity.Password != model.CurrentPassword)
                { throw new CustomException("Current password is incorrect"); }

                entity.SetNewPassword(model.NewPassword);

                await _repository.SaveChanges();
            }
        }

        public async Task Approve(int id, ApproveModel model)
        {
            using (_repository)
            {
                var entity = await _repository.GetQuery<User>(x => x.Id == id, true)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException($"User with Id '{id}' doesn't exist."); }

                entity.Set(entity.Login, entity.Email, model.Role);

                switch (model.Role)
                {
                    case RoleCode.DEVELOPER:
                        entity.SetIsHeadOfCompany(model.IsHeadOfCompany);
                        break;
                    case RoleCode.PROGRAMME_MANAGER:
                        foreach (var programmeId in model.Programmes)
                        {
                            await _repository.Add(new UserProgramme(entity, programmeId));
                        }
                        break;
                    default:
                        break;
                }

                await _repository.SaveChanges();
            }
        }

        public async Task ToggleIsHeadOfCompany(int id)
        {
            using (_repository)
            {
                var entity = await _repository.GetQuery<User>(x => x.Id == id, true)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException($"User with Id '{id}' doesn't exist."); }
                else if (entity.IsHeadOfCompany && !await _repository.Exists<User>(z => z.CompanyId == entity.CompanyId && z.Id != id && z.IsHeadOfCompany))
                { throw new CustomException("At least one Head of Company must exist."); }

                entity.SetIsHeadOfCompany(!entity.IsHeadOfCompany);

                await _repository.SaveChanges();
            }
        }

        public async Task Delete(int id)
        {
            using (_repository)
            {
                var entity = await _repository.GetQuery<User>(x => x.Id == id)
                    .Include(z => z.Programmes)
                    .FirstOrDefaultAsync();

                if (entity.Programmes?.Count > 0)
                { _repository.Delete(entity.Programmes); }

                _repository.Delete(entity);
                await _repository.SaveChanges();
            }
        }

        private async Task MapViewModel(UserViewModel model, User entity)
        {
            entity.Set(model.Login, model.Email, model.Role);
            entity.SetFullName(model.FullName);
            entity.SetCompanyId(model.CompanyId.Value);
            entity.SetIsHeadOfCompany(model.Role == RoleCode.DEVELOPER && model.IsHeadOfCompany);

            if (model.Role == RoleCode.PROGRAMME_MANAGER)
            {
                foreach (var programmeId in model.Programmes)
                {
                    await _repository.Add(new UserProgramme(entity, programmeId));
                };
            }
        }
    }
}
