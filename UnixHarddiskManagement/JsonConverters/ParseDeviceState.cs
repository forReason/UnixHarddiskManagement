using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Serialization;
using UnixHarddiskManagement.Enumerators;

namespace UnixHarddiskManagement.JsonConverters
{
    public static partial class Converter
    {
        public static DeviceState ParseDeviceState(string deviceStateString)
        {
            if (string.IsNullOrEmpty(deviceStateString))
                return default;
            foreach (var item in Enum.GetValues(typeof(DeviceState)))
            {
                var type = (DeviceState)item;
                var memberInfo = type.GetType().GetMember(type.ToString()).FirstOrDefault();
                var attribute = memberInfo?.GetCustomAttribute<JsonPropertyNameAttribute>();

                if (attribute != null && attribute.Name == deviceStateString)
                    return type;
            }

            Debug.WriteLine($"Could not parse DeviceState {deviceStateString}");
            return default; // Or handle unknown types as needed
        }
    }
}
