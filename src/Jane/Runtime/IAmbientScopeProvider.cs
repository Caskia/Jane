using System;

namespace Jane.Runtime
{
    public interface IAmbientScopeProvider<T>
    {
        IDisposable BeginScope(string contextKey, T value);

        T GetValue(string contextKey);
    }
}