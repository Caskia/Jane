using Jane.Extensions;
using JaneConfiguration = Jane.Configurations.Configuration;

namespace Jane.Runtime.Caching.Redis
{
    public class RedisCacheOptions : IRedisCacheOptions
    {
        public RedisCacheOptions()
        {
            ConnectionString = GetDefaultConnectionString();
            DatabaseId = GetDefaultDatabaseId();
        }

        public string ConnectionString { get; set; }
        public int DatabaseId { get; set; }

        private static string GetDefaultConnectionString()
        {
            var connStr = JaneConfiguration.Instance.Root["Redis:ConnectionString"];
            if (connStr == null || connStr.IsNullOrWhiteSpace())
            {
                return "localhost";
            }

            return connStr;
        }

        private static int GetDefaultDatabaseId()
        {
            var appSetting = JaneConfiguration.Instance.Root["Redis:DatabaseId"];
            if (appSetting.IsNullOrEmpty())
            {
                return -1;
            }

            int databaseId;
            if (!int.TryParse(appSetting, out databaseId))
            {
                return -1;
            }

            return databaseId;
        }
    }
}