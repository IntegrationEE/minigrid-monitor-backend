using System;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Monitor.Domain.Base;
using Monitor.Domain.Entities;
using Monitor.Domain.Quartiles;
using Monitor.Infrastructure.Configuration;

namespace Monitor.Infrastructure
{
    public class MinigridDbContext : DbContext
    {
        public MinigridDbContext(DbContextOptions<MinigridDbContext> options)
            : base(options)
        { }

        #region Tables
        public virtual DbSet<ChartConfiguration> ChartConfigurations { get; set; }
        public virtual DbSet<Consumption> Consumptions { get; set; }
        public virtual DbSet<CustomerSatisfaction> CustomerSatisfactions { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Employment> Employments { get; set; }
        public virtual DbSet<FinanceCapex> FinanceCapex { get; set; }
        public virtual DbSet<FinanceOpex> FinanceOpex { get; set; }
        public virtual DbSet<Integration> Integrations { get; set; }
        public virtual DbSet<IntegrationRecord> IntegrationRecords { get; set; }
        public virtual DbSet<IntegrationStep> IntegrationSteps { get; set; }
        public virtual DbSet<LocalGovernmentArea> LocalGovernmentAreas { get; set; }
        public virtual DbSet<MeteringType> MeteringTypes { get; set; }
        public virtual DbSet<NewService> NewServices { get; set; }
        public virtual DbSet<PeopleConnected> PeopleConnected { get; set; }
        public virtual DbSet<Programme> Programmes { get; set; }
        public virtual DbSet<ProgrammeIndicator> ProgrammeIndicators { get; set; }
        public virtual DbSet<ProgrammeIndicatorValue> ProgrammeIndicatorValues { get; set; }
        public virtual DbSet<Revenue> Revenues { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<SiteTechParameter> SiteTechParameters { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Tariff> Tariffs { get; set; }
        public virtual DbSet<Threshold> Thresholds { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProgramme> UserProgrammes { get; set; }
        #endregion

        #region Quartiles
        public virtual DbSet<ConsumptionQuartile> ConsumptionQuartiles { get; set; }
        public virtual DbSet<FinanceOpexQuartile> FinanceOpexQuartiles { get; set; }
        public virtual DbSet<ProgrammeIndicatorValueQuartile> ProgrammeIndicatorValueQuartiles { get; set; }
        public virtual DbSet<RevenueQuartile> RevenueQuartiles { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConversionHelper.Create(modelBuilder);
            PrimaryKeyHelper.Create(modelBuilder);
            UniqueHelper.Create(modelBuilder);
            RelationsHelper.Create(modelBuilder);
            PrecissionHelper.Create(modelBuilder);
            KeylessHelper.Create(modelBuilder);

            modelBuilder.HasPostgresExtension("postgis");
            modelBuilder.UseIdentityColumns();

            //Disable cascade
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                .ToList();

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default, bool acceptAllChangesOnSuccess = true)
        {
            var entries = ChangeTracker.Entries<IBaseEntity>()
                .Where(p => p.State == EntityState.Added || p.State == EntityState.Modified)
                ?.ToList() ?? new List<EntityEntry<IBaseEntity>>();

            var now = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = entry.Entity.Created ?? now;

                        ValidateEntity(entry);
                        break;
                    case EntityState.Modified:
                        entry.Entity.Modified = now;
                        entry.State = EntityState.Modified;

                        ValidateEntity(entry);
                        break;
                    default:
                        break;
                }
            }

            return await SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ValidateEntity(EntityEntry<IBaseEntity> entry)
        {
            entry.Entity.GetType().GetProperties()
                .Where(x =>
                    x.IsDefined(typeof(RequiredAttribute), false) ||
                    x.IsDefined(typeof(StringLengthAttribute), false) ||
                    x.IsDefined(typeof(RegularExpressionAttribute), false)
                )
                .ToList().ForEach(x =>
                {
                    if (x.IsDefined(typeof(RequiredAttribute), false) || x.IsDefined(typeof(RegularExpressionAttribute), false))
                    {
                        ValidateProperty(x, entry);
                    }
                    else if (x.IsDefined(typeof(StringLengthAttribute), false) && entry.Property(x.Name).CurrentValue != null)
                    {
                        ValidateProperty(x, entry);
                    }
                });
        }

        private void ValidateProperty(PropertyInfo x, EntityEntry<IBaseEntity> entry)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(entry.Property(x.Name).CurrentValue);
            var attributes = x.GetCustomAttributes(false).OfType<ValidationAttribute>().ToArray();

            if (!Validator.TryValidateValue(entry.Property(x.Name)?.CurrentValue?.ToString(), context, results, attributes))
            {
                throw new Exception(ReplaceFieldName(results[0].ErrorMessage, x));
            }
        }

        private string ReplaceFieldName(string error, PropertyInfo property)
        {
            if (property.PropertyType == typeof(string))
            {
                return error.Replace("String", $"'{property.Name}'");
            }

            return error;
        }
    }
}