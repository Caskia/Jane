namespace Jane.Tests.BackgroundJobs
{
    public class MyJobArgs
    {
        public MyJobArgs()
        {
        }

        public MyJobArgs(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}