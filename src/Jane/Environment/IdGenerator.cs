using Jane.Runtime.UniqueIdGenerator;
using System;

namespace Jane
{
    public class IdGenerator : IIdGenerator
    {
        #region Fields

        private static Generator idGenerator = null;

        #endregion Fields

        #region Ctor

        public IdGenerator(IMachineManager machineManager)
        {
            idGenerator = new Generator(machineManager.GetMachineId(), new DateTime(2016, 6, 6));
        }

        #endregion Ctor

        public long NextId()
        {
            return (long)idGenerator.NextLong();
        }

        public string NextIdString()
        {
            return idGenerator.Next();
        }
    }
}