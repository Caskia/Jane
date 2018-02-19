namespace Jane.ENode
{
    public interface IENodeConfiguration
    {
        string EventStoreConnectionString { get; set; }

        string EventStoreDatabaseName { get; set; }

        string LockServiceConnectionString { get; set; }

        int LockServiceDatabaseId { get; set; }
    }
}