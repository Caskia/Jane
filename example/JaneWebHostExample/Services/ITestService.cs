﻿using Jane.Dependency;

namespace JaneWebHostExample.Services
{
    public interface ITestService : ITransientDependency
    {
        string GetRandomString();
    }
}