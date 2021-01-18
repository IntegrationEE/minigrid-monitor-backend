using Microsoft.EntityFrameworkCore;
using Monitor.Domain.Quartiles;

namespace Monitor.Infrastructure.Configuration
{
    public class KeylessHelper
    {
        public static void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsumptionQuartile>(x =>
            {
                x.HasNoKey();
                x.ToView("ConsumptionQuartile");

                x.Property(z => z.CommercialQ1).HasColumnName("commercialq1");
                x.Property(z => z.CommercialQ3).HasColumnName("commercialq3");

                x.Property(z => z.PeakLoadQ1).HasColumnName("peakloadq1");
                x.Property(z => z.PeakLoadQ3).HasColumnName("peakloadq3");

                x.Property(z => z.ProductiveQ1).HasColumnName("productiveq1");
                x.Property(z => z.ProductiveQ3).HasColumnName("productiveq3");

                x.Property(z => z.PublicQ1).HasColumnName("publicq1");
                x.Property(z => z.PublicQ3).HasColumnName("publicq3");

                x.Property(z => z.ResidentialQ1).HasColumnName("residentialq1");
                x.Property(z => z.ResidentialQ3).HasColumnName("residentialq3");

                x.Property(z => z.TotalQ1).HasColumnName("totalq1");
                x.Property(z => z.TotalQ3).HasColumnName("totalq3");
            });

            modelBuilder.Entity<FinanceOpexQuartile>(x =>
            {
                x.HasNoKey();
                x.ToView("FinanceOpexQuartile");

                x.Property(z => z.CompanyLevelQ1).HasColumnName("companylevelq1");
                x.Property(z => z.CompanyLevelQ3).HasColumnName("companylevelq3");

                x.Property(z => z.LoanRepaymentsQ1).HasColumnName("loanrepaymentsq1");
                x.Property(z => z.LoanRepaymentsQ3).HasColumnName("loanrepaymentsq3");

                x.Property(z => z.SiteSpecificQ1).HasColumnName("sitespecificq1");
                x.Property(z => z.SiteSpecificQ3).HasColumnName("sitespecificq3");

                x.Property(z => z.TaxesQ1).HasColumnName("taxesq1");
                x.Property(z => z.TaxesQ3).HasColumnName("taxesq3");
            });

            modelBuilder.Entity<RevenueQuartile>(x =>
            {
                x.HasNoKey();
                x.ToView("RevenueQuartile");

                x.Property(z => z.CommercialQ1).HasColumnName("commercialq1");
                x.Property(z => z.CommercialQ3).HasColumnName("commercialq3");

                x.Property(z => z.ProductiveQ1).HasColumnName("productiveq1");
                x.Property(z => z.ProductiveQ3).HasColumnName("productiveq3");

                x.Property(z => z.PublicQ1).HasColumnName("publicq1");
                x.Property(z => z.PublicQ3).HasColumnName("publicq3");

                x.Property(z => z.ResidentialQ1).HasColumnName("residentialq1");
                x.Property(z => z.ResidentialQ3).HasColumnName("residentialq3");
            });

            modelBuilder.Entity<ProgrammeIndicatorValueQuartile>(x =>
            {
                x.HasNoKey();
                x.ToView("ProgrammeIndicatorValueQuartile");

                x.Property(z => z.ValueQ1).HasColumnName("valueq1");
                x.Property(z => z.ValueQ3).HasColumnName("valueq3");
            });
        }
    }
}
