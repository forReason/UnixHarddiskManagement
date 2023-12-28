
namespace UnixHarddiskManagement.Enumerators
{
    using System.Text.Json.Serialization;
    using UnixHarddiskManagement.JsonConverters;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DeviceType
    {
        /// <summary>
        /// default value for unknown types
        /// </summary>
        [StringValue("other"), JsonPropertyName("other")]
        Other = 0,

        [StringValue("disk"), JsonPropertyName("disk")]
        Disk,

        [StringValue("part"), JsonPropertyName("part")]
        Partition,

        [StringValue("loop"), JsonPropertyName("loop")]
        Loop,

        [StringValue("rom"), JsonPropertyName("rom")]
        Rom,

        /// <summary>
        /// Logical Volume
        /// </summary>
        [StringValue("lvm"), JsonPropertyName("lvm")]
        LVM
    }

}
