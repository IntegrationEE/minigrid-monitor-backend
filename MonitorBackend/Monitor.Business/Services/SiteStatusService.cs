using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monitor.Common;
using Monitor.Infrastructure;
using Monitor.Domain.Entities;
using Monitor.Domain.LightModels;

namespace Monitor.Business.Services
{
    public class SiteStatusService : ISiteStatusService
    {
        private readonly IBaseRepository _repository;

        public SiteStatusService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<SiteStatusModel> GetStatus(int id)
        {
            using (_repository)
            {
                var currentDate = DateTime.Today;
                var threeMonths = currentDate.AddMonths(-3);
                var sixMonths = currentDate.AddMonths(-6);

                var result = await _repository.GetQuery<Site>(x => x.Id == id)
                    .Select(x => new SiteStatusModel()
                    {
                        SiteInfo = Constants.GREEN_STATUS,
                        TechnicalSpec = x.TechnicalParameter != null ? Constants.GREEN_STATUS : Constants.RED_STATUS,
                        FinancialDetails = x.FinanceCapex != null ? Constants.GREEN_STATUS : Constants.RED_STATUS,

                        TechnicalIndicators =
                            x.Consumptions.Any(z =>
                                z.Year == currentDate.Year && z.Month >= threeMonths.Month &&
                                (
                                    currentDate.Year == threeMonths.Year ||
                                    (z.Year == threeMonths.Year && z.Month >= threeMonths.Month)
                                )
                            ) ?
                            Constants.GREEN_STATUS :
                            (
                                 x.Consumptions.Any(z =>
                                    z.Year == currentDate.Year && z.Month >= sixMonths.Month &&
                                    (
                                        currentDate.Year == sixMonths.Year ||
                                        (z.Year == sixMonths.Year && z.Month >= sixMonths.Month)
                                    )
                                ) ? Constants.ORANGE_STATUS : Constants.RED_STATUS
                            ),
                        SocialIndicators =
                            x.CustomerSatisfactions.Max(z => z.VisitDate) > threeMonths &&
                            x.PeopleConnected.Max(z => z.VisitDate) > threeMonths &&
                            x.Tariffs.Max(z => z.VisitDate) > threeMonths &&
                            x.Employments.Max(z => z.VisitDate) > threeMonths &&
                            x.NewServices.Max(z => z.VisitDate) > threeMonths ?
                                Constants.GREEN_STATUS :
                                (
                                    x.CustomerSatisfactions.Max(z => z.VisitDate) > sixMonths &&
                                    x.PeopleConnected.Max(z => z.VisitDate) > sixMonths &&
                                    x.Tariffs.Max(z => z.VisitDate) > sixMonths &&
                                    x.Employments.Max(z => z.VisitDate) > sixMonths &&
                                    x.NewServices.Max(z => z.VisitDate) > sixMonths ?
                                        Constants.ORANGE_STATUS : Constants.RED_STATUS
                                ),
                        FinancialIndicators =
                            x.Revenues.Any(z =>
                                z.Year == currentDate.Year && z.Month >= threeMonths.Month &&
                                (
                                    currentDate.Year == threeMonths.Year ||
                                    (z.Year == threeMonths.Year && z.Month >= threeMonths.Month)
                                )
                            ) &&
                            x.FinanceOpex.Any(z =>
                                z.Year == currentDate.Year && z.Month >= threeMonths.Month &&
                                (
                                    currentDate.Year == threeMonths.Year ||
                                    (z.Year == threeMonths.Year && z.Month >= threeMonths.Month)
                                )
                            ) ?
                                Constants.GREEN_STATUS :
                                (
                                    x.Revenues.Any(z =>
                                        z.Year == currentDate.Year && z.Month >= sixMonths.Month &&
                                        (
                                            currentDate.Year == sixMonths.Year ||
                                            (z.Year == sixMonths.Year && z.Month >= sixMonths.Month)
                                        )
                                    ) &&
                                    x.FinanceOpex.Any(z =>
                                        z.Year == currentDate.Year && z.Month >= sixMonths.Month &&
                                        (
                                            currentDate.Year == sixMonths.Year ||
                                            (z.Year == sixMonths.Year && z.Month >= sixMonths.Month)
                                        )
                                    ) ? Constants.ORANGE_STATUS : Constants.RED_STATUS
                                )
                    }).FirstOrDefaultAsync();

                return result;
            }
        }
    }
}
