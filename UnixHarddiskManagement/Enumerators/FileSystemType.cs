using System.Text.Json.Serialization;
using UnixHarddiskManagement.JsonConverters;

namespace UnixHarddiskManagement.Enumerators
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum FileSystemType
    {
        /// <summary>
        /// default value for unknown types
        /// </summary>
        [StringValue("other"), JsonPropertyName("Other")]
        Other = 0,

        [StringValue("NTFS"), JsonPropertyName("NTFS")]
        NTFS,

        [StringValue("FAT32"), JsonPropertyName("FAT32")]
        FAT32,

        [StringValue("exFAT"), JsonPropertyName("exFAT")]
        exFAT,

        [StringValue("ext3"), JsonPropertyName("EXT3")]
        EXT3,

        [StringValue("ext4"), JsonPropertyName("EXT4")]
        EXT4,

        [StringValue("hfs+"), JsonPropertyName("HFS+")]
        HFSPlus,

        [StringValue("apfs"), JsonPropertyName("APFS")]
        APFS,

        [StringValue("xfs"), JsonPropertyName("XFS")]
        XFS,

        [StringValue("btrfs"), JsonPropertyName("Btrfs")]
        Btrfs,

        [StringValue("refs"), JsonPropertyName("ReFS")]
        ReFS,

        [StringValue("squashfs"), JsonPropertyName("SquashFs")]
        SquashFs,
            
        [StringValue("LVM2_member"), JsonPropertyName("LVM2_member")]
        LVM2Member
    }
}
