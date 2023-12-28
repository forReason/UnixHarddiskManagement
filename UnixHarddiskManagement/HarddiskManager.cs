using System.Text.Json;
using UnixCommandInvokeHelper;
using UnixHarddiskManagement.Enumerators;
using UnixHarddiskManagement.Fstab;
using UnixHarddiskManagement.Objects;

namespace UnixHarddiskManagement
{
    public class HarddiskManager
    {
        public HarddiskManager(string username, string? password = null, FileInfo? keyFile = null, string host = "localhost") 
        {
            SshClient = new SshCommandHelper(username, password, keyFile, host);
        }
        SshCommandHelper SshClient;
        /// <summary>
        /// returns all file systems, unfiltered
        /// </summary>
        /// <remarks>
        /// in most cases, its recommended to use GetFileSystems with a filter, except you want to filter by yourself
        /// </remarks>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public FileSystem[] GetAllFileSystems()
        {
            // fetch file system data
            CommandResult result = SshClient.RunCommand("lsblk -O -b --j");
            // error handling
            if (result.Exception != null)
                throw result.Exception;
            if (!string.IsNullOrEmpty(result.Errors))
            {
                throw new Exception(result.Errors);
            }
            // parse json
            using JsonDocument doc = JsonDocument.Parse(result.Output);
            List<FileSystem> fileSystems = new List<FileSystem>();
            foreach (JsonElement hdd in doc.RootElement.GetProperty("blockdevices").EnumerateArray())
            {
                fileSystems.Add(new FileSystem(hdd));
            }
            return fileSystems.ToArray();
        }
        /// <summary>
        /// filters all file systems recursively. Its recommended to provide a file system type, such as "disk" or "partition" for that reason
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public FileSystem[] GetFileSystems(FileSystemFilter? filter = null)
        {
            // fetch file systems
            FileSystem[] all = GetAllFileSystems();
            // early return
            if (filter == null)
                return all;

            List<FileSystem> selected = new List<FileSystem>();
            Stack<FileSystem> systemsToCheck = new Stack<FileSystem>(all);
            // recursive loop
            while (systemsToCheck.Count > 0)
            {
                FileSystem sys = systemsToCheck.Pop();
                // add children to check queue
                if (sys.Children != null)
                {
                    foreach (FileSystem child in sys.Children)
                    {
                        systemsToCheck.Push(child);
                    }
                }
                // apply filter
                if (filter.PassesFilter(sys, false))
                    selected.Add(sys);
            }
            return selected.ToArray();
        }
        /// <summary>
        /// retrieves the fstab file
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public FstabFile GetFSTab()
        {
            // pull fstab file
            CommandResult result = SshClient.RunCommand("cat /etc/fstab");
            if (result.Exception != null)
                throw result.Exception;
            if (!string.IsNullOrEmpty(result.Errors))
            {
                throw new Exception(result.Errors);
            }
            // return file
            return new FstabFile(result.Output);
        }
    }
}
