using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XiangJiang.Core;

namespace XiangJiang.Infrastructure.Serializer.Json
{
    public sealed class JsonHelper
    {
        public static bool IsValidJsonText(string jsonText)
        {
            Checker.Begin().NotNullOrEmpty(jsonText, nameof(jsonText));
            jsonText = jsonText.Trim();
            var result = false;
            if (jsonText.StartsWith("{") && jsonText.EndsWith("}") ||
                jsonText.StartsWith("[") && jsonText.EndsWith("]"))
                try
                {
                    JToken.Parse(jsonText);
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }

            return result;
        }

        public static DataTable ToDataTable(string jsonText)
        {
            Checker.Begin().NotNullOrEmpty(jsonText, nameof(jsonText));
            var jToken = JToken.Parse(jsonText);
            if (jToken is JArray) return JsonConvert.DeserializeObject<DataSet>(jsonText)?.Tables[0];

            var jArray = new JArray {jToken};
            return JsonConvert.DeserializeObject<DataTable>(jArray.ToString());
        }

        public static void ToXmlFile(string jsonText, string xmlFile)
        {
            Checker.Begin().NotNullOrEmpty(jsonText, nameof(jsonText))
                .IsFilePath(xmlFile);
            XNode node = JsonConvert.DeserializeXNode(jsonText, "Root");
            var xmlText = node?.ToString();
            File.WriteAllText(xmlFile, xmlText, Encoding.UTF8);
        }
    }
}