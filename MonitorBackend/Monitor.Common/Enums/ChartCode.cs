using System.ComponentModel;

namespace Monitor.Common.Enums
{
    public enum ChartCode
    {
        // Overview
        [Description("People connected")]
        PEOPLE_CONNECTED = 1,

        [Description("Installed renewable energy capacity")]
        INSTALLED_RENEWABLE,

        [Description("Total investments")]
        INVESTMENTS,

        [Description("Communities connected")]
        COMMUNITIES_CONNECTED,

        [Description("Electricity consumed")]
        ELECTRICITY_CONSUMED,

        [Description("Average tariff")]
        AVERAGE_TARIFF,
        // Social
        [Description("Number of connections")]
        CONNECTIONS,

        [Description("Employments")]
        EMPLOYMENTS,

        [Description("New services")]
        NEW_SERVICES,

        [Description("Customer satisfaction")]
        CUSTOMER_SATISFACTION,
        // Technical
        [Description("Installed capacity")]
        INSTALLED_CAPACITY,

        [Description("Average consumptions")]
        AVERAGE_CONSUMPTIONS,

        [Description("Capacity utilization")]
        CAPACITY_UTILIZATION,

        [Description("Electricity consumption")]
        ELECTRICITY_CONSUMPTION,

        [Description("Daily profile")]
        DAILY_PROFILE,
        // Financial
        [Description("Revenue")]
        REVENUE,

        [Description("Capex")]
        CAPEX,

        [Description("Opex")]
        OPEX,

        [Description("Financing structure")]
        FINANCING_STRUCTURE
    }
}
