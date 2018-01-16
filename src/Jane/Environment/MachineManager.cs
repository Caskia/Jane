using Jane.IO;
using Jane.Runtime.Caching;
using System;
using System.IO;
using System.Text;

namespace Jane
{
    public class MachineManager : IMachineManager
    {
        #region Fields

        private readonly ICacheManager _cacheManager;

        #endregion Fields

        #region Ctor

        public MachineManager(
            ICacheManager cacheManager
            )
        {
            _cacheManager = cacheManager;
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
                machineId = Convert.ToInt16(_cacheManager.GetCache(JaneCacheNames.ApplicationSettings).Increment("machine_id"));
                File.WriteAllText(machineIdFile, machineId.ToString(), Encoding.UTF8);
            }
            return machineId;
        }
    }
}