using System;

namespace Jane
{
    public interface IIdGenerator
    {
        long NextId();

        string NextIdString();

        Guid Guid();
    }
}