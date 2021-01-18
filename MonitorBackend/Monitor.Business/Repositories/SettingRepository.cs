using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitor.Common.Enums;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;

namespace Monitor.Business.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly IBaseRepository _repository;

        public SettingRepository(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> GetCurrency()
        {
            var setting = await _repository.GetQuery<Setting>(x => x.Code == SettingCode.CURRENCY)
                .FirstOrDefaultAsync();

            return setting?.Value;
        }
    }
}
