using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Monitor.Common.Enums;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;
using Monitor.Business.Extensions;

namespace Monitor.Business.Services
{
    public class SettingService : ISettingService
    {
        private readonly IBaseRepository _repository;

        public SettingService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SettingViewModel>> GetAllForFrontend()
        {
            using (_repository)
            {
                var avalaibleCodes = new SettingCode[]
                {
                    SettingCode.ADMINISTRATIVE_UNIT_LABEL,
                    SettingCode.CENTER_POINT,
                    SettingCode.CURRENCY,
                    SettingCode.LGA_LABEL,
                    SettingCode.ORGANISATION_LABEL,
                    SettingCode.SHOW_DAILY_PROFILE
                };

                Expression<Func<Setting, bool>> exp = null;

                foreach (var code in avalaibleCodes)
                {
                    exp = exp == null ?
                        x => x.Code == code :
                        exp.OR(z => z.Code == code);
                }

                return await _repository.GetQuery(exp)
                    .ProjectTo<SettingViewModel>(_repository.Mapper.ConfigurationProvider)
                    .ToListAsync();
            }
        }
    }
}
