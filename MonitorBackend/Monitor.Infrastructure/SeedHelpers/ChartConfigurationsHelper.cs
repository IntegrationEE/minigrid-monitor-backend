using System.Linq;
using Monitor.Common.Enums;
using Monitor.Domain.Entities;

namespace Monitor.Infrastructure.SeedHelpers
{
    public static class ChartConfigurationsHelper
    {
        public static void Seed(MinigridDbContext context)
        {
            var entities = context.ChartConfigurations.Select(z => z.Code).ToList();
            ChartConfiguration entity = null;
            // Overview
            if (!entities.Any(z => z == ChartCode.AVERAGE_TARIFF))
            {
                entity = new ChartConfiguration(ChartCode.AVERAGE_TARIFF, ChartType.OVERVIEW);
                entity.Set("Average residential tariff", string.Empty, "NGN/kWh", false, null);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.PEOPLE_CONNECTED))
            {
                entity = new ChartConfiguration(ChartCode.PEOPLE_CONNECTED, ChartType.OVERVIEW);
                entity.Set("People connected", string.Empty, string.Empty, true, null);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.COMMUNITIES_CONNECTED))
            {
                entity = new ChartConfiguration(ChartCode.COMMUNITIES_CONNECTED, ChartType.OVERVIEW);
                entity.Set("Communities connected", string.Empty, string.Empty, true, null);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.ELECTRICITY_CONSUMED))
            {
                entity = new ChartConfiguration(ChartCode.ELECTRICITY_CONSUMED, ChartType.OVERVIEW);
                entity.Set("Electricity consumed", string.Empty, "Wp", false, ConvertableType.UNIT);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.INSTALLED_RENEWABLE))
            {
                entity = new ChartConfiguration(ChartCode.INSTALLED_RENEWABLE, ChartType.OVERVIEW);
                entity.Set("Installed capacity", "Installed renewable energy capacity", "Wp", true, ConvertableType.UNIT);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.INVESTMENTS))
            {
                entity = new ChartConfiguration(ChartCode.INVESTMENTS, ChartType.OVERVIEW);
                entity.Set("Total investment", string.Empty, "NGN", true, ConvertableType.CURRENCY);

                context.Add(entity);
            }
            // Advanced Analytics
            if (!entities.Any(z => z == ChartCode.AVERAGE_CONSUMPTIONS))
            {
                entity = new ChartConfiguration(ChartCode.AVERAGE_CONSUMPTIONS, ChartType.ADVANCED_ANALYTICS);
                entity.Set("Yearly consumption profile", "Average yearly consumption profile", "Wh", true, ConvertableType.UNIT);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.CAPACITY_UTILIZATION))
            {
                entity = new ChartConfiguration(ChartCode.CAPACITY_UTILIZATION, ChartType.ADVANCED_ANALYTICS);
                entity.Set("Capacity utilization",
                    "Utilization of installed capacity. The factor is defined as peak demand of the system / installed capacity",
                    "%",
                    false,
                    null);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.CAPEX))
            {
                entity = new ChartConfiguration(ChartCode.CAPEX, ChartType.ADVANCED_ANALYTICS);
                entity.Set("Capex structure",
                    "Average Capex structure of the selected portfolio",
                    "%",
                    false,
                    null);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.CONNECTIONS))
            {
                entity = new ChartConfiguration(ChartCode.CONNECTIONS, ChartType.ADVANCED_ANALYTICS);
                entity.Set("Number of connections",
                    "Number of connections per customer group",
                    string.Empty,
                    true,
                    null);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.CUSTOMER_SATISFACTION))
            {
                entity = new ChartConfiguration(ChartCode.CUSTOMER_SATISFACTION, ChartType.ADVANCED_ANALYTICS);
                entity.Set("Customer satisfaction",
                    "Based on following surveyed indicators: networks reliability, developer support, reliability of the payment system",
                    "%",
                    false,
                    null);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.ELECTRICITY_CONSUMPTION))
            {
                entity = new ChartConfiguration(ChartCode.ELECTRICITY_CONSUMPTION, ChartType.ADVANCED_ANALYTICS);
                entity.Set("Elecricity consumption per year",
                    "Aggregated electricity consumption per year",
                    "Wh",
                    true,
                    ConvertableType.UNIT);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.EMPLOYMENTS))
            {
                entity = new ChartConfiguration(ChartCode.EMPLOYMENTS, ChartType.ADVANCED_ANALYTICS);
                entity.Set("Employment created",
                    "Accumulated number of new direct (employed by the developer) and indirect employment in the communities after electrication",
                    string.Empty,
                    true,
                    null);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.FINANCING_STRUCTURE))
            {
                entity = new ChartConfiguration(ChartCode.FINANCING_STRUCTURE, ChartType.ADVANCED_ANALYTICS);
                entity.Set("Financing structure",
                    "Average financing structure of the selected portfolio",
                    "%",
                    false,
                    null);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.INSTALLED_CAPACITY))
            {
                entity = new ChartConfiguration(ChartCode.INSTALLED_CAPACITY, ChartType.ADVANCED_ANALYTICS);
                entity.Set("Installed capacity",
                    "Accumulated installed renewable energy capacity",
                    "W",
                    true,
                    ConvertableType.UNIT);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.NEW_SERVICES))
            {
                entity = new ChartConfiguration(ChartCode.NEW_SERVICES, ChartType.ADVANCED_ANALYTICS);
                entity.Set("New services avalaible",
                    "Accumulated number of new services in the communities after electrication",
                    string.Empty,
                    true,
                    null);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.OPEX))
            {
                entity = new ChartConfiguration(ChartCode.OPEX, ChartType.ADVANCED_ANALYTICS);
                entity.Set("Opex structure",
                    "Average Opex structure of selected portfolio",
                    "%",
                    false,
                    null);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.REVENUE))
            {
                entity = new ChartConfiguration(ChartCode.REVENUE, ChartType.ADVANCED_ANALYTICS);
                entity.Set("Revenues per year",
                    "Aggregated revenues per year",
                    "NGN",
                    true,
                    ConvertableType.CURRENCY);

                context.Add(entity);
            }

            if (!entities.Any(z => z == ChartCode.DAILY_PROFILE))
            {
                entity = new ChartConfiguration(ChartCode.DAILY_PROFILE, ChartType.ADVANCED_ANALYTICS);
                entity.Set("Daily profile",
                    "Average daily consumption profile",
                    "Wh",
                    false,
                    ConvertableType.UNIT);

                context.Add(entity);
            }

            context.SaveChanges();
        }
    }
}
