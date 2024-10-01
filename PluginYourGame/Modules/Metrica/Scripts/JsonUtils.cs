using System.Collections.Generic;
using System.Globalization;

namespace YG.Utils.Metrica
{
    public static class JsonUtils
    {
        public static string ToJson(IDictionary<string, string> dictionary)
        {
            var jsonString = "{";
            var kvpCount = 0;

            foreach (var kvp in dictionary)
            {
                if (string.IsNullOrEmpty(kvp.Key) || string.IsNullOrEmpty(kvp.Value)) continue;
                jsonString += $"\"{kvp.Key}\":{GetValueString(kvp.Value)},";
                kvpCount++;
            }

            if (kvpCount == 0) return string.Empty;

            if (dictionary.Count > 0)
            {
                jsonString = jsonString.Remove(jsonString.Length - 1);
            }

            jsonString += "}";

            return jsonString;
        }

        private static string GetValueString(string value)
        {
            if (int.TryParse(value, out var intValue))
            {
                return intValue.ToString();
            }

            if (float.TryParse(value, out var floatValue))
            {
                return floatValue.ToString(CultureInfo.InvariantCulture);
            }

            if (bool.TryParse(value, out var boolValue))
            {
                return boolValue.ToString().ToLower();
            }

            if (value[0] is '{' && value[value.Length - 1] is '}')
            {
                return value;
            }

            value = value.Replace("\\", "\\\\").Replace("\"", "\\\"");
            return $"\"{value}\"";
        }
    }
}