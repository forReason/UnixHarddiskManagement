
using UnixHarddiskManagement.Enumerators;

namespace UnixHarddiskManagement.JsonConverters
{
    public class StringValueAttribute : Attribute
    {
        public string StringValue { get; private set; }

        public StringValueAttribute(string stringValue)
        {
            this.StringValue = stringValue;
        }
    }
    public static class EnumExtensions
    {
        public static string GetStringValue(this FileSystemType value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

            return attributes?.Length > 0 ? attributes[0].StringValue : null;
        }
    }

}
