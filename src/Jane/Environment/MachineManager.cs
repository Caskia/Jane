using Jane.IO;
using System;
using System.IO;
using System.Text;

namespace Jane
{
    public class MachineManager : IMachineManager
    {
        //#region Fields

        //private readonly IRedisRepositoryBase _redisRepository;

        //#endregion Fields

        #region Ctor

        public MachineManager(
            //IRedisRepositoryBase redisRepository
            )
        {
            //_redisRepository = redisRepository;
        }

        #endregion Ctor

        public short GetMachineId()
        {
            short machineId;
            var configDir = $"{AppDomain.CurrentDomain.BaseDirectory}/Config";
            var machineIdFile = $"{configDir}/machineid.txt";
            DirectoryHelper.CreateIfNotExists(configDir);
            var fileContent = string.Empty;
            if (File.Exists(machineIdFile))
            {
                fileContent = File.ReadAllText(machineIdFile, Encoding.UTF8);
            }
            if (!short.TryParse(fileContent, out machineId))
            {
                //machineId = Convert.ToInt16(_redisRepository.Increment("machine_id"));
                machineId = 1;
                File.WriteAllText(machineIdFile, machineId.ToString(), Encoding.UTF8);
            }
            return machineId;
        }
    }
}