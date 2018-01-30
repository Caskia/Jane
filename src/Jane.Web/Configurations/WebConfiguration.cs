namespace Jane.Configurations
{
    public class WebConfiguration : IWebConfiguration
    {
        public bool SendAllExceptionsToClients { get; set; } = false;
    }
}