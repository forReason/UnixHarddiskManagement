using System.Text;
using UnixCommandInvokeHelper;
using UnixHarddiskManagement.Objects;

namespace UnixHarddiskManagement.Fstab
{
    public class FstabFile
    {
        public FstabFile(string fstabContent = "")
        {
            // load tab entries
            if (fstabContent != "")
            {
                // do some cleaning
                while (fstabContent.Contains("  ")) fstabContent = fstabContent.Replace("  ", " ");
                while (fstabContent.Contains("\n\n")) fstabContent = fstabContent.Replace("\n\n", "\n");
            }
            string[] lines = fstabContent.Split("\n");
            foreach (string line in lines)
            {
                Entries.Add(new FstabEntry(line));
            }
            if (Entries[^1].Comment == "")
                Entries.RemoveAt(Entries.Count - 1);
        }
        
        public List<FstabEntry> Entries = new List<FstabEntry>();

        /// <summary>
        /// Returns the FSTAB Entry index in the list or -1 if the FileSystem was not found
        /// </summary>
        /// <param name="disk"></param>
        /// <returns></returns>
        private int GetFstabIndex(FileSystem disk)
        {
            for (int i = 0; i < Entries.Count; i++)
            {
                string identifier = Entries[i].Identifier.ToUpperInvariant();
                FileSystemFilter filter = new FileSystemFilter();

                if (identifier.StartsWith("UUID="))
                {
                    filter.UUID = identifier.Split('=').Last();
                }
                else if (identifier.StartsWith("/DEV/"))
                {
                    string[] parts = identifier.Split('/');
                    // Assuming that the identifier follows the format /dev/[diskType]/[value]
                    if (parts.Length >= 3)
                    {
                        string diskType = parts[2];
                        string value = string.Join("/", parts.Skip(3)); // Join back the remaining parts if any

                        switch (diskType.ToUpperInvariant())
                        {
                            case "DISK":
                                // This case may need further refinement based on specific subtypes like by-id, by-label, etc.
                                // Example: if identifier is /dev/disk/by-id/xxx, then diskType = "disk" and value = "by-id/xxx"
                                break;
                            case "BY-PARTLABEL":
                                filter.PartitionLabel = value;
                                break;
                            case "BY-PARTUUID":
                                filter.PartitionUUID = value;
                                break;
                            case "BY-PATH":
                                filter.SystemPath = value;
                                break;
                            case "BY-UUID":
                                filter.UUID = value;
                                break;
                        }
                    }
                }

                if (filter.PassesFilter(disk))
                    return i;
            }
            return -1;
        }
        public string CreateEntry(FileSystem disk, SshCommandHelper client, string sudoPwd = "",
            string mountDir = "/mnt/", string mountName = "disk", FstabMountingOption[] options = null, FstabPassOption pass = FstabPassOption.Sequential)
        {
            if (GetFstabIndex(disk) != -1)
            {
                throw new AccessViolationException("Disk is already included!");
            }
            // get mountpoint name
            string mountDirectory = "/mnt/";
            mountName = mountDirectory + mountName;
            string finalMountPoint = GetNextAvailableMountPoint(mountName);
            // create mountpoint directory
            CommandResult mkdir = client.RunCommandSudo($"mkdir -p {finalMountPoint}" , sudoPwd);
            if (mkdir.Exception != null)
            {
                throw mkdir.Exception;
            }
            if (!string.IsNullOrEmpty(mkdir.Errors))
            {
                throw new Exception(mkdir.Errors);
            }
            // default mount options
            if (options == null)
            {
                options = new FstabMountingOption[] { FstabMountingOption.Defaults };
            }
            // create fstab entry
            FstabEntry entry = new FstabEntry(
                disk.UUID,
                finalMountPoint,
                disk.FileSystemType,
                options,
                pass
                );
            this.Entries.Add(entry);
            SaveFile(client,sudoPwd);
            return finalMountPoint;
        }
        public void RemoveEntry(FileSystem disk, SshCommandHelper client, string sudoPwd = "")
        {
            int index = GetFstabIndex(disk);
            if (index == -1)
            {
                throw new AccessViolationException("Disk is not included!");
            }
            this.Entries.RemoveAt(index);
            SaveFile(client, sudoPwd);
        }
        // TODO: remove fstab entry
        private string GetNextAvailableMountPoint(string targetMountName)
        {
            int mountIndex = 0;
            bool mountPointExists = true;
            while (mountPointExists)
            {
                mountIndex++;
                string potentialMountName = targetMountName+ mountIndex.ToString();
                mountPointExists = false;
                foreach (FstabEntry entry in Entries)
                {
                    if (entry.MountingPoint == null)
                    {
                        continue;
                    }
                    if (entry.MountingPoint.Equals(potentialMountName))
                    {
                        mountPointExists=true;
                        break;
                    }
                }
            }
            return targetMountName + mountIndex.ToString();
        }
        private void SaveFile(SshCommandHelper client, string sudoPwd = "")
        {
            StringBuilder fstabstring = new StringBuilder();
            foreach (FstabEntry entry in Entries)
            {
                fstabstring.Append(entry.ToString());
                fstabstring.Append('\n');
            }
            CommandResult appendResult = client.RunCommandSudo($"echo \"{fstabstring.ToString()}\" > ~/fstab",sudoPwd);
            if (appendResult.Exception != null)
                throw appendResult.Exception;
            if (appendResult.Errors != null)
                throw new Exception(appendResult.Errors);
            CommandResult replaceResult = client.RunCommandSudo($"mv ~/fstab /etc/fstab",sudoPwd);
            if (replaceResult.Exception != null)
                throw replaceResult.Exception;
            if (replaceResult.Errors != null)
                throw new Exception(replaceResult.Errors);
        }
    }
}
