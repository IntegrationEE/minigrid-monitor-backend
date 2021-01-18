using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Monitor.Common.Enums;
using Monitor.Domain.Entities;
using Monitor.Infrastructure.SeedHelpers;

namespace Monitor.Infrastructure
{
    public static class DatabaseInitializer
    {
        public static void Seed(MinigridDbContext context)
        {
            CreateFunctions(context);

            var settings = context.Settings.Select(x => x.Code).ToList();

            //seed the app settings into the database, please fill in parameters for a migration
            if (!settings.Any(x => x == SettingCode.SMTP_LOGIN))
                context.Settings.Add(new Setting(SettingCode.SMTP_LOGIN, ""));
            if (!settings.Any(x => x == SettingCode.SMTP_PASSWORD))
                context.Settings.Add(new Setting(SettingCode.SMTP_PASSWORD, ""));
            if (!settings.Any(x => x == SettingCode.SMTP_PORT))
                context.Settings.Add(new Setting(SettingCode.SMTP_PORT, ""));
            if (!settings.Any(x => x == SettingCode.SMTP_SERVER))
                context.Settings.Add(new Setting(SettingCode.SMTP_SERVER, ""));
            if (!settings.Any(x => x == SettingCode.SMTP_USE_SSL))
                context.Settings.Add(new Setting(SettingCode.SMTP_USE_SSL, ""));
            if (!settings.Any(x => x == SettingCode.SMTP_FROM_ADDRESS))
                context.Settings.Add(new Setting(SettingCode.SMTP_FROM_ADDRESS, ""));
            //type in currency short code like USD or EUR
            if (!settings.Any(x => x == SettingCode.CURRENCY))
                context.Settings.Add(new Setting(SettingCode.CURRENCY, ""));
            //centerpoint of the visible map in the dashboard in LAT/LONG 
            if (!settings.Any(x => x == SettingCode.CENTER_POINT))
                context.Settings.Add(new Setting(SettingCode.CENTER_POINT, "9.082, 8.6753"));
            if (!settings.Any(x => x == SettingCode.ADMINISTRATIVE_UNIT_LABEL))
                context.Settings.Add(new Setting(SettingCode.ADMINISTRATIVE_UNIT_LABEL, "State"));
            if (!settings.Any(x => x == SettingCode.SHOW_DAILY_PROFILE))
                context.Settings.Add(new Setting(SettingCode.SHOW_DAILY_PROFILE, true.ToString()));
            if (!settings.Any(x => x == SettingCode.LGA_LABEL))
                context.Settings.Add(new Setting(SettingCode.LGA_LABEL, "LGA"));
            if (!settings.Any(x => x == SettingCode.ORGANISATION_LABEL))
                context.Settings.Add(new Setting(SettingCode.ORGANISATION_LABEL, "Company"));
            //average peoples per household -> here it's 5, important for some calculations
            if (!settings.Any(x => x == SettingCode.PEOPLE_IN_HOUSEHOLD))
                context.Settings.Add(new Setting(SettingCode.PEOPLE_IN_HOUSEHOLD, 5.ToString()));

            context.SaveChanges();

            if (!context.Users.Any())
            {
                //set admin cred
                var password = ""; 
                var entity = new User();
                entity.Set("", "", RoleCode.ADMINISTRATOR);
                entity.SetFullName("");
                entity.SetNewPassword(password);
                entity.ToggleIsActive();

                context.Users.Add(entity);

                context.SaveChanges();
            }

            StatesHelper.Seed(context);
            LocalGovernmentAreasHelper.Seed(context);

            if (!context.Programmes.Any())
            {
                context.Programmes.Add(new Programme("TestProgramme"));

                context.SaveChanges();
            }

            if (!context.MeteringTypes.Any())
            {
                context.MeteringTypes.Add(new MeteringType("Prepaid"));
                context.MeteringTypes.Add(new MeteringType("Postpaid"));
                context.MeteringTypes.Add(new MeteringType("Other"));

                context.SaveChanges();
            }

            var thresholds = context.Thresholds.Select(z => z.Code).ToList();

            if (!thresholds.Any(z => z == ThresholdCode.RENEWABLE_CAPACITY))
                context.Add(new Threshold(ThresholdCode.RENEWABLE_CAPACITY, 0.1m, 999));
            if (!thresholds.Any(z => z == ThresholdCode.CONVENTIONAL_CAPACITY))
                context.Add(new Threshold(ThresholdCode.CONVENTIONAL_CAPACITY, 0.1m, 999));
            if (!thresholds.Any(z => z == ThresholdCode.STORAGE_CAPACITY))
                context.Add(new Threshold(ThresholdCode.STORAGE_CAPACITY, 0.1m, 999));
            if (!thresholds.Any(z => z == ThresholdCode.GRID_LENGTH))
                context.Add(new Threshold(ThresholdCode.GRID_LENGTH, 0.1m, 99));

            context.SaveChanges();

            ChartConfigurationsHelper.Seed(context);
        }

        private static void CreateFunctions(MinigridDbContext context)
        {
            // Functions
            string prefix = "Functions";

            Create(context, prefix, "getConsumptionQuartiles");
            Create(context, prefix, "getFinanceOpexQuartiles");
            Create(context, prefix, "getRevenueQuartiles");
            Create(context, prefix, "getProgrammeIndicatorValueQuartiles");
        }

        private static void Create(MinigridDbContext context, string prefix, string fileName)
        {
            ExecuteScripts(context, $"{prefix}/Drop_{fileName}");
            ExecuteScripts(context, $"{prefix}/Create_{fileName}");
        }

        private static void ExecuteScripts(MinigridDbContext context, string scriptName)
        {
            context.Database.ExecuteSqlRaw(File.ReadAllText($"{AppContext.BaseDirectory}/SQL/{scriptName}.sql"));
        }
    }
}
