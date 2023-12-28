using System.Diagnostics;
using System.Reflection;
using System.Text.Json.Serialization;
using UnixHarddiskManagement.Enumerators;

namespace UnixHarddiskManagement.JsonConverters
{
    public static partial class Converter
    {
        public static FileSystemType ParseFileSystemType(string fsTypeString)
        {
            if (string.IsNullOrEmpty(fsTypeString))
                return default;

            foreach (var item in Enum.GetValues(typeof(FileSystemType)))
            {
                var type = (FileSystemType)item;
                var memberInfo = type.GetType().GetMember(type.ToString()).FirstOrDefault();
                var attribute = memberInfo?.GetCustomAttribute<JsonPropertyNameAttribute>();

                if (attribute != null && string.Equals(attribute.Name, fsTypeString, StringComparison.OrdinalIgnoreCase))
                    return type;
            }

            Debug.WriteLine($"Could not parse FileSystemType \"{fsTypeString}\" !");
            return default; // Or handle unknown types as needed
        }

    }
}
