using System;

namespace Jane
{
    public static class JaneEnvironment
    {
        public static string GetEnvironment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        public static bool IsDevelopment()
        {
            return IsEnvironment("Development");
        }

        public static bool IsEnvironment(string name)
        {
            return GetEnvironment() == name;
        }

        public static bool IsProduction()
        {
            return IsEnvironment("Production");
        }

        public static bool IsStaging()
        {
            return IsEnvironment("Staging");
        }
    }
}