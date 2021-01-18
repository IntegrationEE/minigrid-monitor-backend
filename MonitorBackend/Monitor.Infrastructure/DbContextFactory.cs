using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Monitor.Infrastructure
{
    public class DbContextFactory : IDesignTimeDbContextFactory<MinigridDbContext>
    {
        public MinigridDbContext CreateDbContext(string[] args)
        {
            //Connection String for Migration, enter your cred when you want to run a migration
            var connectionString = "Host=;Database=;Username=;Password=";

            var optionsBuilder = new DbContextOptionsBuilder<MinigridDbContext>();
            optionsBuilder.UseNpgsql(connectionString, o => o.UseNetTopologySuite());

            return new MinigridDbContext(optionsBuilder.Options);
        }
    }
}
