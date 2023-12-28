namespace UnixHarddiskManagement.Enumerators
{
    using System.Text.Json.Serialization;
    using UnixHarddiskManagement.JsonConverters;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DeviceState
    {
        [StringValue("Unknown"), JsonPropertyName("unknown")]
        Unknown = 0,

        [StringValue("Running"), JsonPropertyName("running")]
        Running,

        [StringValue("Sleeping"), JsonPropertyName("sleeping")]
        Sleeping,

        [StringValue("Standby"), JsonPropertyName("standby")]
        Standby,

        [StringValue("Offline"), JsonPropertyName("offline")]
        Offline,

        [StringValue("Suspended"), JsonPropertyName("suspended")]
        Suspended,

        [StringValue("Faulted"), JsonPropertyName("faulted")]
        Faulted,

        [StringValue("Idle"), JsonPropertyName("idle")]
        Idle
    }

}
