using AutoMapper;
using System;

namespace Jane.AutoMapper
{
    public interface IAutoMapperRegister
    {
        void CreateMappings(Action<IMapper> registerMapper);
    }
}