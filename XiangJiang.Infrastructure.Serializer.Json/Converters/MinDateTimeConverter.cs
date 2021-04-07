using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace XiangJiang.Infrastructure.Serializer.Json.Converters
{
    /// <summary>
    ///     用于处理DateTime是Null情况，赋值DateTime.MinValue
    ///     <para>字段属性增加： [JsonConverter(typeof(MinDateTimeConverter))]</para>
    /// </summary>
    /// 时间：2016/7/20 10:49
    /// 备注：
    public sealed class MinDateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.Value == null) return DateTime.MinValue;

            return (DateTime)reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var dateTimeValue = (DateTime)value;
            if (dateTimeValue == DateTime.MinValue)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(value);
        }
    }
}