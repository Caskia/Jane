using Jane.Dependency;

namespace JaneENodeGenericHostExample.Services
{
    public interface ITestService : ITransientDependency
    {
        string GetRandomString();
    }
}