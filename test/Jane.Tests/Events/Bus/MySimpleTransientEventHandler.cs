using System;
using Jane.Events.Bus.Handlers;

namespace Jane.Tests.Events.Bus
{
    public class MySimpleTransientEventHandler : IEventHandler<MySimpleEventData>, IDisposable
    {
        public static int DisposeCount { get; set; }
        public static int HandleCount { get; set; }

        public void Dispose()
        {
            ++DisposeCount;
        }

        public void HandleEvent(MySimpleEventData eventData)
        {
            ++HandleCount;
        }
    }
}