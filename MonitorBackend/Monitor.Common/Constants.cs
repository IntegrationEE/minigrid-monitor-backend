using System;

namespace Monitor.Common
{
    public class Constants
    {
        // Claims
        public const string USER_ID = "userId";
        public const string USER_ROLE = "userRole";
        public const string EMAIL = "email";
        public const string USERNAME = "username";
        public const string COMPANY = "company";
        public const string IS_ANONYMUOS = "isAnonymous";
        public const string IS_HEAD_OF_COMPANY = "isHeadOfCompany";
        // Site status
        public const string GREEN_STATUS = "green";
        public const string ORANGE_STATUS = "orange";
        public const string RED_STATUS = "red";
        // Graph labels
        public const string CAPACITY = "capacity";
        public const string GRID_LENGTH = "Grid length";

        public const string COMMERCIAL_CONN = "Commercial connections";
        public const string RESIDENTIAL_CONN = "Residential connections";
        public const string PRODUCTIVE_CONN = "Productive connections";
        public const string PUBLIC_CONN = "Public connections";

        public const string COMMERCIAL_TARIFF = "Commercial tariff";
        public const string RESIDENTIAL_TARIFF = "Residential tariff";
        public const string PRODUCTIVE_TARIFF = "Productive tariff";
        public const string PUBLIC_TARIFF = "Public tariff";

        public const string UNIT_OF_CAPACITY = "kW";
        public const string UNIT_OF_BATTERY_CAPACITY = "kWh";
        public const string UNIT_OF_GRID = "km";
        // Content Types
        public const string PNG_CONTENT_TYPE = "image/png";
        public const string XLS_CONTENT_TYPE = "application/vnd.ms-excel";
        public const string CSV_CONTENT_TYPE = "text/csv";
        // Settings
        public const string CONFIG_GUEST_SECRET = "GuestSecret";
        public const string CONFIG_VERSION = "Version";
        public const string CONFIG_AUTHORITY = "Authority";
        public const string CONFIG_CONNECTION_STRING = "ConnectionString";
        public const string CONFIG_IDENTITY_SERVER = "IdentityServerUrl";
        public const string CONFIG_REQUIRE_HTTPS = "RequireHttps";
        public const string CONFIG_ALLOWED_ORIGINS = "AllowedOrigins";
        // Factors
        public const int THOUSAND = 1000;
        public const int HUNDRED = 100;
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        // Content Types
        public const string IMAGE_PNG_CONTENT_TYPE = "image/png";
    }
}
