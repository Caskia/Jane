using System;
using System.Collections.Generic;

namespace Jane.Configurations
{
    public class ValidationConfiguration : IValidationConfiguration
    {
        public ValidationConfiguration()
        {
            IgnoredTypes = new List<Type>();
        }

        public List<Type> IgnoredTypes { get; }
    }
}