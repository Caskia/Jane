using System;
using System.Collections.Generic;

namespace Jane.Configurations
{
    public interface IValidationConfiguration
    {
        List<Type> IgnoredTypes { get; }
    }
}