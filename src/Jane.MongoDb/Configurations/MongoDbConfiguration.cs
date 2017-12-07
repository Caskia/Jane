namespace Jane.Configurations
{
    public class MongoDbConfiguration : IMongoDbConfiguration
    {
        public MongoDbConfiguration()
        {
            LoadConfigurationFromSetting();
        }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        private void LoadConfigurationFromSetting()
        {
            ConnectionString = Configuration.Instance.Root["MongoDb:ConnectionString"];
            DatabaseName = Configuration.Instance.Root["MongoDb:DatabaseName"];
        }
    }
}