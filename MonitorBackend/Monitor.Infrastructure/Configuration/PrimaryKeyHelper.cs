using Microsoft.EntityFrameworkCore;
using Monitor.Domain.Entities;

namespace Monitor.Infrastructure.Configuration
{
    public class PrimaryKeyHelper
    {
        public static void Create(ModelBuilder modelBuilder)
        {
            #region Set Start Value
            modelBuilder.Entity<Company>()
                .Property(x => x.Id)
                .HasIdentityOptions(startValue: 10);

            modelBuilder.Entity<Site>()
                .Property(x => x.Id)
                .HasIdentityOptions(startValue: 10);

            modelBuilder.Entity<Consumption>()
                .Property(x => x.Id)
                .HasIdentityOptions(startValue: 50);

            modelBuilder.Entity<Employment>()
                .Property(x => x.Id)
                .HasIdentityOptions(startValue: 8);

            modelBuilder.Entity<FinanceCapex>()
                .Property(x => x.Id)
                .HasIdentityOptions(startValue: 4);

            modelBuilder.Entity<FinanceOpex>()
                .Property(x => x.Id)
                .HasIdentityOptions(startValue: 50);

            modelBuilder.Entity<NewService>()
                .Property(x => x.Id)
                .HasIdentityOptions(startValue: 8);

            modelBuilder.Entity<PeopleConnected>()
                .Property(x => x.Id)
                .HasIdentityOptions(startValue: 8);

            modelBuilder.Entity<Revenue>()
                .Property(x => x.Id)
                .HasIdentityOptions(startValue: 50);

            modelBuilder.Entity<SiteTechParameter>()
                .Property(x => x.Id)
                .HasIdentityOptions(startValue: 10);

            modelBuilder.Entity<Tariff>()
                .Property(x => x.Id)
                .HasIdentityOptions(startValue: 8);

            modelBuilder.Entity<CustomerSatisfaction>()
                .Property(x => x.Id)
                .HasIdentityOptions(startValue: 36);
            #endregion

            modelBuilder.Entity<Setting>()
                .HasKey(x => x.Code);

            modelBuilder.Entity<UserProgramme>()
                .HasKey(x => new { x.UserId, x.ProgrammeId });
        }
    }
}
