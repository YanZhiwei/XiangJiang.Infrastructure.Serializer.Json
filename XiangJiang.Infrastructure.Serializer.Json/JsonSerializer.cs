using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using XiangJiang.Core;
using XiangJiang.Infrastructure.Abstractions;

namespace XiangJiang.Infrastructure.Serializer.Json
{
    /// <summary>
    ///     Json 序列化与反序列化
    /// </summary>
    public class JsonSerializer : ISerializer
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public JsonSerializer(JsonSerializerSettings serializerSettings = null)
        {
            _serializerSettings = serializerSettings ?? DefaultSettings();
        }

        #region Methods

        /// <summary>
        ///     反序列化
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="data">需要反序列化字符串</param>
        /// <returns>反序列化</returns>
        public T Deserialize<T>(string data)
        {
            Checker.Begin().NotNullOrEmpty(data, nameof(data));
            T deserializedType = default;
            var serializer = new Newtonsoft.Json.JsonSerializer();
            using (var reader = new StringReader(data))
            {
                using (var jsonReader = new JsonTextReader(reader))
                {
                    deserializedType = serializer.Deserialize<T>(jsonReader);
                }
            }

            return deserializedType;
        }

        /// <summary>
        ///     序列化
        /// </summary>
        /// <param name="serializeObject">需要序列化对象</param>
        /// <returns>Json字符串</returns>
        public string Serialize(object serializeObject)
        {
            Checker.Begin().NotNull(serializeObject, nameof(serializeObject));
            return JsonConvert.SerializeObject(serializeObject, _serializerSettings);
        }

        private JsonSerializerSettings DefaultSettings()
        {
            var serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return serializerSettings;
        }

        #endregion Methods
    }
}