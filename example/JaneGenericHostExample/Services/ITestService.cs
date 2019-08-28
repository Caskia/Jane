using Jane.Dependency;

namespace JaneGenericHostExample.Services
{
    public interface ITestService : ITransientDependency
    {
        string GetRandomString();
    }
}