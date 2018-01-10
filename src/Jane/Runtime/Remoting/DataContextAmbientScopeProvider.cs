using Jane.Extensions;
using Jane.Logging;
using System;
using System.Collections.Concurrent;

namespace Jane.Runtime.Remoting
{
    public class DataContextAmbientScopeProvider<T> : IAmbientScopeProvider<T>
    {
        private static readonly ConcurrentDictionary<string, ScopeItem> ScopeDictionary = new ConcurrentDictionary<string, ScopeItem>();
        private readonly IAmbientDataContext _dataContext;

        public DataContextAmbientScopeProvider(
            IAmbientDataContext dataContext
            )
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

            Logger = LogHelper.Logger;
        }

        private ILogger Logger { get; set; }

        public IDisposable BeginScope(string contextKey, T value)
        {
            var item = new ScopeItem(value, GetCurrentItem(contextKey));

            if (!ScopeDictionary.TryAdd(item.Id, item))
            {
                throw new JaneException("Can not add item! ScopeDictionary.TryAdd returns false!");
            }

            _dataContext.SetData(contextKey, item.Id);

            return new DisposeAction(() =>
            {
                ScopeDictionary.TryRemove(item.Id, out item);

                if (item.Outer == null)
                {
                    _dataContext.SetData(contextKey, null);
                    return;
                }

                _dataContext.SetData(contextKey, item.Outer.Id);
            });
        }

        public T GetValue(string contextKey)
        {
            var item = GetCurrentItem(contextKey);
            if (item == null)
            {
                return default(T);
            }

            return item.Value;
        }

        private ScopeItem GetCurrentItem(string contextKey)
        {
            var objKey = _dataContext.GetData(contextKey) as string;
            return objKey != null ? ScopeDictionary.GetOrDefault(objKey) : null;
        }

        private class ScopeItem
        {
            public ScopeItem(T value, ScopeItem outer = null)
            {
                Id = Guid.NewGuid().ToString();

                Value = value;
                Outer = outer;
            }

            public string Id { get; }

            public ScopeItem Outer { get; }

            public T Value { get; }
        }
    }
}