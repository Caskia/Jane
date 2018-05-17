using Jane.Events.Bus;

namespace Jane.Tests.Events.Bus
{
    public class MySimpleEventData : EventData
    {
        public MySimpleEventData(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }
}