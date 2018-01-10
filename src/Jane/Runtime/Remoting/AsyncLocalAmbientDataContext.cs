﻿using Jane.Dependency;
using System.Collections.Concurrent;
using System.Threading;

namespace Jane.Runtime.Remoting
{
    public class AsyncLocalAmbientDataContext : IAmbientDataContext
    {
        private static readonly ConcurrentDictionary<string, AsyncLocal<object>> AsyncLocalDictionary = new ConcurrentDictionary<string, AsyncLocal<object>>();

        public object GetData(string key)
        {
            var asyncLocal = AsyncLocalDictionary.GetOrAdd(key, (k) => new AsyncLocal<object>());
            return asyncLocal.Value;
        }

        public void SetData(string key, object value)
        {
            var asyncLocal = AsyncLocalDictionary.GetOrAdd(key, (k) => new AsyncLocal<object>());
            asyncLocal.Value = value;
        }
    }
}