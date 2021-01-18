using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitor.Common.Enums;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.ViewModels;

namespace Monitor.Business.Services
{
    public class CustomerSatisfactionService : BaseVisitSiteManageService<CustomerSatisfactionViewModel, CustomerSatisfaction>, ICustomerSatisfactionService
    {
        public CustomerSatisfactionService(IBaseRepository repository)
            : base(repository)
        { }

        public override async Task<CustomerSatisfactionViewModel> Get(int siteId, DateTime date)
        {
            using (Repository)
            {
                return await GetViewModel(siteId, date);
            }
        }

        public override async Task<CustomerSatisfactionViewModel> Save(int siteId, CustomerSatisfactionViewModel model)
        {
            using (Repository)
            {
                var entities = await Repository.GetQuery<CustomerSatisfaction>(x => x.VisitDate == model.VisitDate && x.SiteId == siteId)
                    .ToListAsync();

                if (entities.Count > 0)
                {
                    Repository.Delete(entities);
                }

                await Repository.Add(new CustomerSatisfaction(siteId, model.VisitDate, model.VerySatisfied, SatisfactionType.VERY_SATISFIED));
                await Repository.Add(new CustomerSatisfaction(siteId, model.VisitDate, model.SomehowSatisfied, SatisfactionType.SOMEHOW_SATISFIED));
                await Repository.Add(new CustomerSatisfaction(siteId, model.VisitDate, model.NeitherSatisfiedNorUnsatisfied, SatisfactionType.NEITHER_SATISFIED_NOR_UNSATISFIED));
                await Repository.Add(new CustomerSatisfaction(siteId, model.VisitDate, model.SomehowUnsatisfied, SatisfactionType.SOMEHOW_UNSATISFIED));
                await Repository.Add(new CustomerSatisfaction(siteId, model.VisitDate, model.VeryUnsatisfied, SatisfactionType.VERY_UNSATISFIED));

                await Repository.SaveChanges();

                return await GetViewModel(siteId, model.VisitDate);
            }
        }

        protected override async Task<CustomerSatisfactionViewModel> GetViewModel(int siteId, DateTime date)
        {
            var data = await Repository.GetQuery<CustomerSatisfaction>(x => x.VisitDate == date && x.SiteId == siteId)
                .GroupBy(z => z.Type)
                .Select(z => new
                {
                    Type = z.Key,
                    Satisfaction = z.Sum(y => y.Satisfaction)
                }).ToListAsync();

            return data.Count > 0 ? new CustomerSatisfactionViewModel
            {
                VisitDate = date,
                VerySatisfied = data.First(z => z.Type == SatisfactionType.VERY_SATISFIED).Satisfaction,
                SomehowSatisfied = data.First(z => z.Type == SatisfactionType.SOMEHOW_SATISFIED).Satisfaction,
                NeitherSatisfiedNorUnsatisfied = data.First(z => z.Type == SatisfactionType.NEITHER_SATISFIED_NOR_UNSATISFIED).Satisfaction,
                SomehowUnsatisfied = data.First(z => z.Type == SatisfactionType.SOMEHOW_UNSATISFIED).Satisfaction,
                VeryUnsatisfied = data.First(z => z.Type == SatisfactionType.VERY_UNSATISFIED).Satisfaction,
            } : null;
        }
    }
}
