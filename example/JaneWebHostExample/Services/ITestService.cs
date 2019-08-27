using Jane.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JaneWebHostExample.Services
{
    public interface ITestService : ITransientDependency
    {
        string GetRandomString();
    }
}