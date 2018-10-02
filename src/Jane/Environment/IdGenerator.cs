using Jane.Runtime.Guids;
using System;

namespace Jane
{
    public class IdGenerator : IIdGenerator
    {
        #region Fields

        private static Runtime.UniqueIdGenerator.IdGenerator idGenerator = null;
        private readonly SequentialGuidGenerator _guidGenerator;

        #endregion Fields

        #region Ctor

        public IdGenerator(IMachineManager machineManager, SequentialGuidGenerator guidGenerator)
        {
            idGenerator = new Runtime.UniqueIdGenerator.IdGenerator(machineManager.GetMachineId(), new DateTime(2017, 12, 8, 0, 0, 0, DateTimeKind.Utc));
            _guidGenerator = guidGenerator;
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

        public Guid Guid()
        {
            return _guidGenerator.Create();
        }
    }
}