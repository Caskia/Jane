namespace Jane.ENode
{
    public interface IENodeConfiguration
    {
        string AggregateSnapshotConnectionString { get; set; }

        string AggregateSnapshotDatabaseName { get; set; }

        string EventStoreConnectionString { get; set; }

        string EventStoreDatabaseName { get; set; }
        string LockServiceConnectionString { get; set; }

        int LockServiceDatabaseId { get; set; }
    }
}