namespace Jane.Configurations
{
    public class JaneWebConfiguration : IJaneWebConfiguration
    {
        public bool SendAllExceptionsToClients { get; set; } = false;
    }
}