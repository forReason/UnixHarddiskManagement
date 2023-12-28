using System.Reflection;
using System.Text.Json.Serialization;
using UnixHarddiskManagement.Enumerators;

namespace UnixHarddiskManagement.JsonConverters
{
    public static partial class Converter
    {
        public static DeviceType ParseDeviceType(string deviceTypeString)
        {
            foreach (var item in Enum.GetValues(typeof(DeviceType)))
            {
                var type = (DeviceType)item;
                var memberInfo = type.GetType().GetMember(type.ToString()).FirstOrDefault();
                var attribute = memberInfo?.GetCustomAttribute<JsonPropertyNameAttribute>();

                if (attribute != null && attribute.Name == deviceTypeString)
                    return type;
            }

            return default; // Or handle unknown types as needed
        }
    }
}
