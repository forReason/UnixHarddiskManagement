using UnixHarddiskManagement.Enumerators;

namespace UnixHarddiskManagement.Objects
{
    public class FileSystemFilter
    {
        public string Name { get; set; }
        public string Vendor { get; set; }
        public DeviceType[] DeviceTypes { get; set; }
        public string PartitionType { get; set; }
        public string PartitionLabel { get; set; }
        public string PartitionUUID { get; set; }
        public string MountPoint { get; set; }
        public string FilesystemLabel { get; set; }
        public string UUID { get; set; }
        public bool? Removable { get; set; }
        public bool? ReadOnly { get; set; }
        public string SystemPath { get; set; }

        // Size filters
        public ByteSize? TotalSizeMinimum { get; set; }
        public ByteSize? TotalSizeMaximum { get; set; }
        public ByteSize? AvailableSizeMinimum { get; set; }
        public ByteSize? AvailableSizeMaximum { get; set; }
        public double? AvailableSizeMinimumPercent { get; set; }
        public double? AvailableSizeMaximumPercent { get; set; }
        public ByteSize? UsedSizeMinimum { get; set; }
        public ByteSize? UsedSizeMaximum { get; set; }
        public double? UsedSizeMinimumPercent { get; set; }
        public double? UsedSizeMaximumPercent { get; set; }

        // File system type filter
        public FileSystemType[] FileSystemTypes { get; set; }

        // Device State
        public DeviceState[] DeviceState { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceSerial { get; set; }

        /// <summary>
        /// returns an enumerator which only matches the current filter
        /// </summary>
        /// <param name="fileSystems"></param>
        /// <returns></returns>
        public IEnumerable<FileSystem> FilterFileSystems(IEnumerable<FileSystem> fileSystems)
        {
            return fileSystems.Where(fs => PassesFilter(fs));
        }

        /// <summary>
        /// verifies an individual file system matches the desired Filter
        /// </summary>
        /// <param name="fileSystem"></param>
        /// <param name="scanChildren">searches the children (partitions) as well and if one partition matches returns true</param>
        /// <returns></returns>
        public bool PassesFilter(FileSystem fileSystem, bool scanChildren = true)
        {
            if (fileSystem.Children != null && scanChildren)
            {
                foreach (FileSystem kid in fileSystem.Children)
                {
                    if (PassesFilter(kid))
                    {
                        return true;
                    }
                }
            }
            
            // String and enum checks (cheap and high fail-rate)
            if (!string.IsNullOrEmpty(Name) && fileSystem.Name != Name)
                return false;
            if (!string.IsNullOrEmpty(Vendor) && fileSystem.DeviceVendor != Vendor)
                return false;
            if (DeviceTypes != null && !DeviceTypes.Contains(fileSystem.DeviceType))
                return false;
            if (!string.IsNullOrEmpty(PartitionType) && fileSystem.PartitionType != PartitionType)
                return false;
            if (!string.IsNullOrEmpty(PartitionLabel) && fileSystem.PartitionLabel != PartitionLabel)
                return false;
            if (!string.IsNullOrEmpty(MountPoint) && fileSystem.MountPoint != MountPoint)
                return false;
            if (!string.IsNullOrEmpty(FilesystemLabel) && fileSystem.FilesystemLabel != FilesystemLabel)
                return false;
            if (!string.IsNullOrEmpty(UUID) && fileSystem.UUID != UUID)
            if (!string.IsNullOrEmpty(PartitionUUID) && fileSystem.PartitionUUID != PartitionUUID)
                return false;
            if (!string.IsNullOrEmpty(DeviceModel) && fileSystem.DeviceModel != DeviceModel)
                return false;
            if (!string.IsNullOrEmpty(DeviceSerial) && fileSystem.DeviceSerial != DeviceSerial)
                return false;
            if (DeviceState != null && !DeviceState.Contains(fileSystem.DeviceState))
                return false;
            if (!string.IsNullOrEmpty(SystemPath) && !SystemPath.Contains(fileSystem.SystemPath))
                return false;

            // Boolean checks
            if (Removable.HasValue && fileSystem.Removable != Removable.Value)
                return false;
            if (ReadOnly.HasValue && fileSystem.ReadOnly != ReadOnly.Value)
                return false;

            // Size filters (more expensive due to calculations)
            // Exclude if DeviceSize is null or outside the specified range
            if (TotalSizeMinimum != null && (fileSystem.DeviceSize == null || fileSystem.DeviceSize <= TotalSizeMinimum.Bytes))
                return false;
            if (TotalSizeMaximum != null && (fileSystem.DeviceSize == null || fileSystem.DeviceSize >= TotalSizeMaximum.Bytes))
                return false;

            // Exclude if AvailableBytes is null or outside the specified range
            if (AvailableSizeMinimum != null && (fileSystem.AvailableBytes == null || fileSystem.AvailableBytes <= AvailableSizeMinimum.Bytes))
                return false;
            if (AvailableSizeMaximum != null && (fileSystem.AvailableBytes == null || fileSystem.AvailableBytes >= AvailableSizeMaximum.Bytes))
                return false;

            // Exclude if UsedBytes is null or outside the specified range
            if (UsedSizeMinimum != null && (fileSystem.UsedBytes == null || fileSystem.UsedBytes <= UsedSizeMinimum.Bytes))
                return false;
            if (UsedSizeMaximum != null && (fileSystem.UsedBytes == null || fileSystem.UsedBytes >= UsedSizeMaximum.Bytes))
                return false;


            // Percentage based filters
            // Ensure TotalBytes is not null and not zero before performing division
            if (fileSystem.TotalBytes == null || fileSystem.TotalBytes == 0)
                return false;

            // Exclude if AvailableBytes is null or outside the specified percentage range
            if (AvailableSizeMinimumPercent.HasValue &&
                (fileSystem.AvailableBytes == null ||
                (double)fileSystem.AvailableBytes / fileSystem.TotalBytes <= AvailableSizeMinimumPercent.Value))
                return false;
            if (AvailableSizeMaximumPercent.HasValue &&
                (fileSystem.AvailableBytes == null ||
                (double)fileSystem.AvailableBytes / fileSystem.TotalBytes >= AvailableSizeMaximumPercent.Value))
                return false;

            // Exclude if UsedBytes is null or outside the specified percentage range
            if (UsedSizeMinimumPercent.HasValue &&
                (fileSystem.UsedBytes == null ||
                (double)fileSystem.UsedBytes / fileSystem.TotalBytes <= UsedSizeMinimumPercent.Value))
                return false;
            if (UsedSizeMaximumPercent.HasValue &&
                (fileSystem.UsedBytes == null ||
                (double)fileSystem.UsedBytes / fileSystem.TotalBytes >= UsedSizeMaximumPercent.Value))
                return false;


            return true;
        }
    }
}
