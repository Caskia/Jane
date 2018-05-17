using System;
using System.Threading.Tasks;
using Jane.Events.Bus.Handlers;

namespace Jane.Tests.Events.Bus
{
    public class MySimpleTransientAsyncEventHandler : IAsyncEventHandler<MySimpleEventData>, IDisposable
    {
        public static int DisposeCount { get; set; }
        public static int HandleCount { get; set; }

        public void Dispose()
        {
            ++DisposeCount;
        }

        public Task HandleEventAsync(MySimpleEventData eventData)
        {
            ++HandleCount;
            return Task.FromResult(0);
        }
    }
}