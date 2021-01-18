using Microsoft.EntityFrameworkCore;
using Monitor.Domain.Entities;

namespace Monitor.Infrastructure.Configuration
{
    public static class PrecissionHelper
    {
        public static void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consumption>()
                .Property(z => z.Commercial)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<Consumption>()
                .Property(z => z.Public)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<Consumption>()
                .Property(z => z.Residential)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<Consumption>()
                .Property(z => z.Productive)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<Consumption>()
                .Property(z => z.PeakLoad)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<Consumption>()
                .Property(z => z.Total)
                .HasColumnType("decimal(14, 2)");

            modelBuilder.Entity<FinanceCapex>()
                .Property(z => z.Commissioning)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<FinanceCapex>()
                .Property(z => z.CustomerInstallation)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<FinanceCapex>()
                .Property(z => z.Distribution)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<FinanceCapex>()
                .Property(z => z.FinancingDebt)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<FinanceCapex>()
                .Property(z => z.FinancingEquity)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<FinanceCapex>()
                .Property(z => z.FinancingGrant)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<FinanceCapex>()
                .Property(z => z.Generation)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<FinanceCapex>()
                .Property(z => z.Logistics)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<FinanceCapex>()
                .Property(z => z.SiteDevelopment)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<FinanceCapex>()
                .Property(z => z.Taxes)
                .HasColumnType("decimal(12, 2)");

            modelBuilder.Entity<FinanceOpex>()
                .Property(z => z.SiteSpecific)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<FinanceOpex>()
                .Property(z => z.Taxes)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<FinanceOpex>()
                .Property(z => z.LoanRepayments)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<FinanceOpex>()
                .Property(z => z.CompanyLevel)
                .HasColumnType("decimal(12, 2)");

            modelBuilder.Entity<ProgrammeIndicatorValue>()
                .Property(z => z.Value)
                .HasColumnType("decimal(12, 2)");

            modelBuilder.Entity<Revenue>()
                .Property(z => z.Commercial)
                .HasColumnType("decimal(14, 2)");
            modelBuilder.Entity<Revenue>()
                .Property(z => z.Productive)
                .HasColumnType("decimal(14, 2)");
            modelBuilder.Entity<Revenue>()
                .Property(z => z.Public)
                .HasColumnType("decimal(14, 2)");
            modelBuilder.Entity<Revenue>()
                .Property(z => z.Residential)
                .HasColumnType("decimal(14, 2)");

            modelBuilder.Entity<SiteTechParameter>()
                .Property(z => z.RenewableCapacity)
                .HasColumnType("decimal(8, 2)");
            modelBuilder.Entity<SiteTechParameter>()
                .Property(z => z.StorageCapacity)
                .HasColumnType("decimal(8, 2)");
            modelBuilder.Entity<SiteTechParameter>()
                .Property(z => z.ConventionalCapacity)
                .HasColumnType("decimal(8, 2)");
            modelBuilder.Entity<SiteTechParameter>()
                .Property(z => z.GridLength)
                .HasColumnType("decimal(8, 2)");

            modelBuilder.Entity<Tariff>()
                .Property(z => z.Commercial)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<Tariff>()
                .Property(z => z.Productive)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<Tariff>()
                .Property(z => z.Public)
                .HasColumnType("decimal(12, 2)");
            modelBuilder.Entity<Tariff>()
                .Property(z => z.Residential)
                .HasColumnType("decimal(12, 2)");

            modelBuilder.Entity<Threshold>()
                .Property(z => z.Min)
                .HasColumnType("decimal(8, 4)");
            modelBuilder.Entity<Threshold>()
                .Property(z => z.Max)
                .HasColumnType("decimal(8, 4)");
        }
    }
}
