namespace Jane.ENode
{
    public class ENodeConfiguration : IENodeConfiguration
    {
        public string EventStoreConnectionString { get; set; }

        public string EventStoreDatabaseName { get; set; }

        public string LockServiceConnectionString { get; set; }

        public int LockServiceDatabaseId { get; set; }
    }
}