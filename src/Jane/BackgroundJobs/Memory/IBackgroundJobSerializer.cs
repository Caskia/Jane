using System;

namespace Jane.BackgroundJobs
{
    public interface IBackgroundJobSerializer
    {
        string Serialize(object obj);

        object Deserialize(string value, Type type);
    }
}
