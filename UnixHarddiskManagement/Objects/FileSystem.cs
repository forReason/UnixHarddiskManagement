
using System.Text.Json;
using System.Xml.Linq;
using UnixHarddiskManagement.Enumerators;
using UnixHarddiskManagement.JsonConverters;

namespace UnixHarddiskManagement.Objects
{
    public partial class FileSystem
    {
        public FileSystem(JsonElement input)
        {
            // string values
            this.Name = input.GetProperty("name").GetString();
            this.KernelDeviceName = input.GetProperty("kname").GetString();
            this.ParentKernelDeviceName = input.TryGetProperty("pkname", out var pkname) ? pkname.GetString() : null;
            this.SystemPath = input.GetProperty("path").GetString();
            this.DriverIdentifier = input.GetProperty("maj:min").GetString();
            this.FileSystemVersion = input.TryGetProperty("fsver", out var fsver) ? fsver.GetString() : null;
            this.MountPoint = input.GetProperty("mountpoint").GetString();
            this.FilesystemLabel = input.GetProperty("label").GetString();
            this.UUID = input.GetProperty("uuid").GetString();
            this.PartitionTableUUID = input.GetProperty("ptuuid").GetString();
            this.PartitionTableType = input.GetProperty("pttype").GetString();
            this.PartitionType = input.GetProperty("parttype").GetString();
            this.PartitionTypeName = input.TryGetProperty("parttypename", out var parttypename) ? parttypename.GetString() : null;
            this.PartitionLabel = input.GetProperty("partlabel").GetString();
            this.PartitionUUID = input.GetProperty("partuuid").GetString();
            this.PartitionFlags = input.GetProperty("partflags").GetString();
            this.DeviceModel = input.GetProperty("model").GetString();
            this.DeviceSerial = input.GetProperty("serial").GetString();
            this.DeviceVersion = input.GetProperty("rev").GetString();
            this.DeviceVendor = input.GetProperty("vendor").GetString();
            this.PermissionOwner = input.GetProperty("owner").GetString();
            this.PermissionGroup = input.GetProperty("group").GetString();
            this.PermissionString = input.GetProperty("mode").GetString();
            this.IOShedulerName = input.GetProperty("sched").GetString();
            this.UniqueStorageIdentifier = input.GetProperty("wwn").GetString();
            this.ICSI_HostChannelTargetLun = input.GetProperty("hctl").GetString();
            this.DeviceTransportType = input.GetProperty("tran").GetString();
            this.SubSystems = input.GetProperty("subsystems").GetString();
            this.ZoneModel = input.GetProperty("zoned").GetString();
            // long values
            this.DeviceSize = input.GetProperty("size").GetUInt64();
            this.TotalBytes = ReadUlong(input.GetProperty("fssize"));
            this.UsedBytes = ReadUlong(input.GetProperty("fsused"));
            this.AvailableBytes = ReadUlong(input.GetProperty("fsavail"));
            if (AvailableBytes == null && DeviceSize != null && UsedBytes != null)
            {
                AvailableBytes = DeviceSize;
                AvailableBytes -= UsedBytes;
            }
            this.ReadAhead = input.GetProperty("ra").GetUInt64();
            this.AlignmentOffset = input.GetProperty("alignment").GetUInt64();
            this.MinIOSize = input.GetProperty("min-io").GetUInt64();
            this.OptimumIOSize = input.GetProperty("opt-io").GetUInt64();
            this.PhysicalSectorSize = input.GetProperty("phy-sec").GetUInt64();
            this.VirtualSectorSize = input.GetProperty("log-sec").GetUInt64();
            this.RequestQueueSize = input.GetProperty("rq-size").GetUInt64();
            this.DiscardAlignmentOffset = input.GetProperty("disc-aln").GetUInt64();
            this.DiscardGranularity = input.GetProperty("disc-gran").GetUInt64();
            this.DiscardMaxBytes = input.GetProperty("disc-max").GetUInt64();
            this.WriteSameMaxBytes = input.GetProperty("wsame").GetUInt64();
            // booleans
            this.DiscardZeroesData = input.GetProperty("disc-zero").GetBoolean();
            this.Removable = input.GetProperty("rm").GetBoolean();
            this.ReadOnly =input.GetProperty("ro").GetBoolean();
            this.HotPlug = input.GetProperty("hotplug").GetBoolean();
            this.RotationalDevice =input.GetProperty("rota").GetBoolean();
            this.RandomnessAdder = input.GetProperty("rand").GetBoolean();
            if (input.TryGetProperty("dax", out var dax))
            {
                this.DaxCapable = dax.GetBoolean();
            }
            // custom enums
            var deviceTypeString = input.GetProperty("type").GetString();
            this.DeviceType = Converter.ParseDeviceType(deviceTypeString);

            var fsTypeString = input.GetProperty("fstype").GetString();
            this.FileSystemType = Converter.ParseFileSystemType(fsTypeString);

            var deviceStateString = input.GetProperty("state").GetString();
            this.DeviceState = Converter.ParseDeviceState(deviceStateString);

            // load children
            this.Children = new List<FileSystem>();
            if (input.TryGetProperty("children", out var children))
            {
                foreach (JsonElement kid in children.EnumerateArray())
                {
                    this.Children.Add(new FileSystem(kid));
                }
            }
        }
        /// <summary>
        /// Device name; eg sda
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// for the most part the same as Name
        /// </summary>
        public string KernelDeviceName { get; set; }
        /// <summary>
        /// type of the File System. EG ext4
        /// </summary>
        public FileSystemType FileSystemType { get; set; }
        /// <summary>
        /// the path of the file system. This is not the mounting point
        /// e.g. /dev/sda
        /// </summary>
        public string SystemPath { get; set; }
        public string DriverIdentifier { get; set; }
        public string? FileSystemVersion { get; set; }
        /// <summary>
        /// the active mounting path such as /mnt/example
        /// </summary>
        public string MountPoint { get; set; }
        /// <summary>
        /// the label of the file system as shown in windows
        /// </summary>
        public string FilesystemLabel { get; set; }
        public string UUID { get; set; }
        public string UniqueStorageIdentifier { get; set; }
        public string PartitionTableUUID { get; set; }
        public string PartitionTableType { get; set; }
        public string PartitionType { get; set; }
        public string PartitionTypeName { get; set; }
        public string PartitionLabel { get; set; }
        public string PartitionUUID { get; set; }
        public string PartitionFlags { get; set; }
        public string DeviceModel { get; set; }
        public DeviceType DeviceType { get; set; }
        public string DeviceSerial { get; set; }
        public DeviceState DeviceState { get; set; }
        public string DeviceVersion { get; set; }
        public string DeviceVendor { get; set; }
        public string PermissionOwner { get; set; }
        public string PermissionGroup { get; set; }
        public string PermissionString { get; set; }
        public string IOShedulerName { get; set; }
        public string ParentKernelDeviceName { get; set; }
        public string ICSI_HostChannelTargetLun { get; set; }
        public string DeviceTransportType { get; set; }
        public string SubSystems { get; set; }
        public string ZoneModel { get; set; }
        /// <summary>
        /// represents the total amount of Bytes of a physical device
        /// </summary>
        public ulong DeviceSize { get; set; }
        private ulong? _availableBytes;
        private ulong? _totalBytes;
        private ulong? _usedBytes;

        public ulong? AvailableBytes
        {
            get
            {
                if (_availableBytes != null || this.Children == null)
                {
                    return _availableBytes;
                }
                // Calculate and cache if null
                ulong bytes = 0;
                bool gotValue = false;
                foreach (FileSystem kid in this.Children)
                {
                    if (kid.AvailableBytes != null)
                    {
                        bytes += kid.AvailableBytes.Value;
                        gotValue = true;
                    }
                }
                if (gotValue)
                    _availableBytes = bytes;
                return _availableBytes;
            }
            set
            {
                _availableBytes = value;
            }
        }

        public ulong? TotalBytes
        {
            get
            {
                if (_totalBytes != null || this.Children == null)
                {
                    return _totalBytes;
                }
                // Calculate and cache if null
                ulong bytes = 0;
                bool gotValue = false;
                foreach (FileSystem kid in this.Children)
                {
                    if (kid.TotalBytes != null)
                    {
                        bytes += kid.TotalBytes.Value;
                        gotValue = true;
                    }
                }
                if (gotValue)
                    _totalBytes = bytes;
                return _totalBytes;
            }
            set
            {
                _totalBytes = value;
            }
        }

        public ulong? UsedBytes
        {
            get
            {
                if (_usedBytes != null || this.Children == null)
                {
                    return _usedBytes;
                }
                // Calculate and cache if null
                ulong bytes = 0;
                bool gotValue = false;
                foreach(FileSystem kid in this.Children)
                {
                    if (kid.UsedBytes != null)
                    {
                        bytes += kid.UsedBytes.Value;
                        gotValue = true;
                    }   
                }
                if (gotValue)
                    _usedBytes = bytes;
                return _usedBytes;
            }
            set
            {
                _usedBytes = value;
            }
        }

        public ulong AlignmentOffset { get; set; }
        public ulong ReadAhead { get; set; }
        public ulong MinIOSize { get; set; }
        public ulong OptimumIOSize { get; set; }
        public ulong PhysicalSectorSize { get; set; }
        public ulong VirtualSectorSize { get; set; }
        public ulong RequestQueueSize { get; set; }
        public ulong DiscardAlignmentOffset { get; set; }
        public ulong DiscardGranularity { get; set; }
        public ulong DiscardMaxBytes { get; set; }
        public ulong WriteSameMaxBytes { get; set; }
        public bool DiscardZeroesData { get; set; }
        public bool Removable { get; set; }
        public bool ReadOnly { get; set; }
        public bool HotPlug { get; set; }
        public bool RotationalDevice { get; set; }
        public bool RandomnessAdder { get; set; }
        public bool? DaxCapable { get; set; }
        public List<FileSystem> Children { get; set; }
        /// <summary>
        /// calculated property evaluating how full a drive is
        /// </summary>
        public double? UsedPercent {
            get
            {
                if (TotalBytes == null || TotalBytes == 0)
                {
                    TotalBytes = 0;
                    UsedBytes = 0;
                    foreach (FileSystem kid in this.Children)
                    {
                        TotalBytes += kid.TotalBytes;
                        UsedBytes += kid.UsedBytes;
                    }
                }
                if (TotalBytes == 0)
                {
                    return 0;
                }
                double value = (double)(UsedBytes / (decimal)TotalBytes);
                return Math.Round(value * 100,2);
            }
        }
        private ulong ParseUlong(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return 0;
            return ulong.TryParse(input, out ulong result) ? result : 0;
        }
        private ulong? ReadUlong(JsonElement input)
        {
            if (input.ValueKind == JsonValueKind.Null)
            {
                return null;
            }
            if (input.ValueKind == JsonValueKind.String)
            {
                return ParseUlong(input.GetString());
            }

            if (input.TryGetUInt64(out ulong result))
            {
                return result;
            }

            throw new InvalidOperationException("Invalid format for 'size'");
        }

    }
}
