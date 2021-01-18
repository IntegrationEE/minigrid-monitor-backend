using Microsoft.EntityFrameworkCore;
using Monitor.Domain.Entities;

namespace Monitor.Infrastructure.Configuration
{
    public class UniqueHelper
    {
        public static void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChartConfiguration>()
               .HasIndex(z => z.Code)
               .IsUnique();

            modelBuilder.Entity<Company>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Consumption>()
                .HasIndex(x => new { x.Year, x.Month, x.SiteId })
                .IsUnique();

            modelBuilder.Entity<CustomerSatisfaction>()
                .HasIndex(x => new { x.SiteId, x.VisitDate, x.Type })
                .IsUnique();

            modelBuilder.Entity<Employment>()
                .HasIndex(x => new { x.SiteId, x.VisitDate })
                .IsUnique();

            modelBuilder.Entity<FinanceOpex>()
                .HasIndex(x => new { x.Year, x.Month, x.SiteId })
                .IsUnique();

            modelBuilder.Entity<IntegrationRecord>()
                .HasIndex(z => new { z.IntegrationId, z.Status })
                .IsUnique()
                .HasFilter("public.\"IntegrationRecords\".\"Status\" = 'RUN_CREATED'");

            modelBuilder.Entity<IntegrationStep>()
                .HasIndex(z => new { z.IntegrationId, z.Ordinal })
                .IsUnique()
                .HasFilter("public.\"IntegrationSteps\".\"IsArchive\" = 0");

            modelBuilder.Entity<LocalGovernmentArea>()
                .HasIndex(x => new { x.StateId, x.Name })
                .IsUnique();

            modelBuilder.Entity<MeteringType>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<NewService>()
                .HasIndex(x => new { x.SiteId, x.VisitDate })
                .IsUnique();

            modelBuilder.Entity<PeopleConnected>()
                .HasIndex(x => new { x.SiteId, x.VisitDate })
                .IsUnique();

            modelBuilder.Entity<Programme>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<ProgrammeIndicatorValue>()
                .HasIndex(x => new { x.ProgrammeIndicatorId, x.Month, x.Year })
                .IsUnique();

            modelBuilder.Entity<Revenue>()
                .HasIndex(x => new { x.Year, x.Month, x.SiteId })
                .IsUnique();

            modelBuilder.Entity<State>()
                .HasIndex(x => x.Name)
                .IsUnique();

            modelBuilder.Entity<Tariff>()
                .HasIndex(x => new { x.SiteId, x.VisitDate })
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(x => x.Login)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();
        }
    }
}
