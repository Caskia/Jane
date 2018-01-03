using System;
using System.Collections.Generic;
using AutoMapper;

namespace Jane.AutoMapper
{
    public class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        public AutoMapperConfiguration()
        {
            UseStaticMapper = true;
            Configurators = new List<Action<IMapperConfigurationExpression>>();
        }

        public List<Action<IMapperConfigurationExpression>> Configurators { get; }

        public bool UseStaticMapper { get; set; }
    }
}