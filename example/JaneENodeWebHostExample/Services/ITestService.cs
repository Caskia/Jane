using Jane.Dependency;

namespace JaneENodeWebHostExample.Services
{
    public interface ITestService : ITransientDependency
    {
        string GetRandomString();
    }
}