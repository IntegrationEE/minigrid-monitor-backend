using System;
using Microsoft.EntityFrameworkCore;
using Monitor.Common.Enums;
using Monitor.Domain.Entities;

namespace Monitor.Infrastructure.Configuration
{
    public static class ConversionHelper
    {
        public static void Create(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<ChartCode>();
            modelBuilder.HasPostgresEnum<ChartType>();
            modelBuilder.HasPostgresEnum<ConventionalTechnology>();
            modelBuilder.HasPostgresEnum<ConvertableType>();
            modelBuilder.HasPostgresEnum<GridConnectionType>();
            modelBuilder.HasPostgresEnum<IntegrationStatusCode>();
            modelBuilder.HasPostgresEnum<RenewableTechnology>();
            modelBuilder.HasPostgresEnum<RoleCode>();
            modelBuilder.HasPostgresEnum<SatisfactionType>();
            modelBuilder.HasPostgresEnum<SettingCode>();
            modelBuilder.HasPostgresEnum<StorageTechnology>();
            modelBuilder.HasPostgresEnum<ThresholdCode>();

            modelBuilder.Entity<ChartConfiguration>()
                .Property(x => x.Code)
                .HasConversion(v => v.ToString(), v => (ChartCode)Enum.Parse(typeof(ChartCode), v));
            modelBuilder.Entity<ChartConfiguration>()
                .Property(x => x.Type)
                .HasConversion(v => v.ToString(), v => (ChartType)Enum.Parse(typeof(ChartType), v));
            modelBuilder.Entity<ChartConfiguration>()
                .Property(x => x.Convertable)
                .HasConversion(
                    v => v.HasValue ? v.ToString() : null,
                    v => !string.IsNullOrWhiteSpace(v) ? (ConvertableType?)Enum.Parse(typeof(ConvertableType), v) : null
                );

            modelBuilder.Entity<CustomerSatisfaction>()
                .Property(x => x.Type)
                .HasConversion(v => v.ToString(), v => (SatisfactionType)Enum.Parse(typeof(SatisfactionType), v));

            modelBuilder.Entity<IntegrationRecord>()
                .Property(z => z.Status)
                .HasConversion(v => v.ToString(), v => (IntegrationStatusCode)Enum.Parse(typeof(IntegrationStatusCode), v));

            modelBuilder.Entity<User>()
                .Property(x => x.Role)
                .HasConversion(v => v.ToString(), v => (RoleCode)Enum.Parse(typeof(RoleCode), v));

            modelBuilder.Entity<Setting>()
                .Property(x => x.Code)
                .HasConversion(v => v.ToString(), v => (SettingCode)Enum.Parse(typeof(SettingCode), v));

            modelBuilder.Entity<SiteTechParameter>()
                .Property(x => x.RenewableTechnology)
                .HasConversion(v => v.ToString(), v => (RenewableTechnology)Enum.Parse(typeof(RenewableTechnology), v));
            modelBuilder.Entity<SiteTechParameter>()
                .Property(x => x.ConventionalTechnology)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToString() : null,
                    v => !string.IsNullOrWhiteSpace(v) ? (ConventionalTechnology?)Enum.Parse(typeof(ConventionalTechnology), v) : null
                );
            modelBuilder.Entity<SiteTechParameter>()
                .Property(x => x.StorageTechnology)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToString() : null,
                    v => !string.IsNullOrWhiteSpace(v) ? (StorageTechnology?)Enum.Parse(typeof(StorageTechnology), v) : null
                );
            modelBuilder.Entity<SiteTechParameter>()
                .Property(x => x.GridConnection)
                .HasConversion(v => v.ToString(), v => (GridConnectionType)Enum.Parse(typeof(GridConnectionType), v));

            modelBuilder.Entity<Threshold>()
                .Property(x => x.Code)
                .HasConversion(v => v.ToString(), v => (ThresholdCode)Enum.Parse(typeof(ThresholdCode), v));
        }
    }
}
