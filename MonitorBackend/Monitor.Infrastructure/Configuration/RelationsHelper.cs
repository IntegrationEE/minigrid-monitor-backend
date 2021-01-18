using Microsoft.EntityFrameworkCore;
using Monitor.Domain.Entities;

namespace Monitor.Infrastructure.Configuration
{
    public static class RelationsHelper
    {
        public static void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consumption>()
                .HasOne(p => p.Site)
                .WithMany(b => b.Consumptions)
                .HasForeignKey(p => p.SiteId);

            modelBuilder.Entity<Employment>()
                .HasOne(p => p.Site)
                .WithMany(b => b.Employments)
                .HasForeignKey(p => p.SiteId);

            modelBuilder.Entity<FinanceCapex>()
                .HasOne(p => p.Site)
                .WithOne(b => b.FinanceCapex)
                .HasForeignKey<FinanceCapex>(p => p.SiteId);

            modelBuilder.Entity<FinanceOpex>()
                .HasOne(p => p.Site)
                .WithMany(b => b.FinanceOpex)
                .HasForeignKey(p => p.SiteId);

            modelBuilder.Entity<NewService>()
                .HasOne(p => p.Site)
                .WithMany(b => b.NewServices)
                .HasForeignKey(p => p.SiteId);

            modelBuilder.Entity<PeopleConnected>()
                .HasOne(p => p.Site)
                .WithMany(b => b.PeopleConnected)
                .HasForeignKey(p => p.SiteId);

            modelBuilder.Entity<ProgrammeIndicator>()
                .HasOne(z => z.Programme)
                .WithMany(z => z.Indicators)
                .HasForeignKey(z => z.ProgrammeId);

            modelBuilder.Entity<ProgrammeIndicatorValue>()
                .HasOne(z => z.ProgrammeIndicator)
                .WithMany(z => z.Values)
                .HasForeignKey(z => z.ProgrammeIndicatorId);

            modelBuilder.Entity<Revenue>()
                .HasOne(p => p.Site)
                .WithMany(b => b.Revenues)
                .HasForeignKey(p => p.SiteId);

            modelBuilder.Entity<SiteTechParameter>()
               .HasOne(a => a.Site)
               .WithOne(b => b.TechnicalParameter)
               .HasForeignKey<SiteTechParameter>(b => b.SiteId);

            modelBuilder.Entity<Tariff>()
                .HasOne(p => p.Site)
                .WithMany(b => b.Tariffs)
                .HasForeignKey(p => p.SiteId);

            modelBuilder.Entity<User>()
                .HasOne(p => p.Company)
                .WithMany(b => b.Users)
                .HasForeignKey(p => p.CompanyId);

            modelBuilder.Entity<UserProgramme>()
                .HasOne(x => x.User)
                .WithMany(z => z.Programmes)
                .HasForeignKey(z => z.UserId);
            modelBuilder.Entity<UserProgramme>()
                .HasOne(x => x.Programme)
                .WithMany(z => z.Users)
                .HasForeignKey(z => z.ProgrammeId);
        }
    }
}
