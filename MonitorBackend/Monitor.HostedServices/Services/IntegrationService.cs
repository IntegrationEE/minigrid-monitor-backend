using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.HostedServices.Models;

namespace Monitor.HostedServices
{
    internal class IntegrationService
    {
        private readonly IBaseRepository _repository;

        public IntegrationService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<IntegrationModel>> GetAll()
        {
            return await _repository.GetQuery<Integration>(z => z.IsActive && z.Steps.Count > 0)
                .Select(z => new IntegrationModel
                {
                    Id = z.Id,
                    Interval = z.Interval,
                    QuestionHash = z.QuestionHash,
                    Token = z.Token,
                    Steps = z.Steps
                        .Where(z => !z.IsArchive)
                        .OrderBy(x => x.Ordinal)
                        .Select(x => new StepModel
                        {
                            Ordinal = x.Ordinal,
                            Function = x.Function,
                            Name = x.Name,
                            Id = x.Id
                        })
                }).ToListAsync();
        }
    }
}
