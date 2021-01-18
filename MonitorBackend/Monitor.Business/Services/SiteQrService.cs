using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitor.Common;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Business.Common;
using Monitor.Business.Helpers;

namespace Monitor.Business.Services
{
    public class SiteQrService : ISiteQrService
    {
        private readonly IBaseRepository _repository;

        public SiteQrService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<byte[]> Get(int id)
        {
            using (_repository)
            {
                var data = await _repository.GetQuery<Site>(z => z.Id == id)
                    .Select(z => new { z.QrCode })
                    .FirstOrDefaultAsync();

                if (data == null)
                { throw new CustomException($"Entity {nameof(Site)} with id: '{id}' does not exist"); }

                var qrGenerator = new QrGenerator();

                return qrGenerator.Generate(new QrLabelModel(data.QrCode));
            }
        }

        public async Task Update(int id)
        {
            using (_repository)
            {
                var entity = await _repository.GetQuery<Site>(x => x.Id == id, true)
                    .FirstOrDefaultAsync();

                if (entity == null)
                { throw new CustomException($"Entity {nameof(Site)} with id: '{id}' does not exist"); }

                entity.UpdateQrCode();

                await _repository.SaveChanges();
            }
        }
    }
}
