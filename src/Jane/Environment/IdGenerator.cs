using System;

namespace Jane
{
    public class IdGenerator : IIdGenerator
    {
        #region Fields

        private static Runtime.UniqueIdGenerator.IdGenerator idGenerator = null;

        #endregion Fields

        #region Ctor

        public IdGenerator(IMachineManager machineManager)
        {
            idGenerator = new Runtime.UniqueIdGenerator.IdGenerator(machineManager.GetMachineId(), new DateTime(2017, 12, 8, 0, 0, 0, DateTimeKind.Utc));
        }

        #endregion Ctor

        public long NextId()
        {
            return idGenerator.CreateId();
        }

        public string NextIdString()
        {
            return idGenerator.CreateId().ToString();
        }
    }
}