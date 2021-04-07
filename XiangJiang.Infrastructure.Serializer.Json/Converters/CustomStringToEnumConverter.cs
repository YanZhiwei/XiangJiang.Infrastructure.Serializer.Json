using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace XiangJiang.Infrastructure.Serializer.Json.Converters
{
    /// <summary>
    ///     用于处理字符串转成枚举
    ///     <para>字段属性增加： [JsonConverter(typeof(CustomStringToEnumConverter<Priority>))]</para>
    ///     <para>枚举上标记： [EnumMember(Value = "Low")] </para>
    /// </summary>
    /// 时间：2016/7/20 10:49
    /// 备注：
    public class CustomStringToEnumConverter<T> : StringEnumConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            if (string.IsNullOrEmpty(reader.Value?.ToString())) return null;
            try
            {
                return EnumExtensions.GetValueFromEnumMember<T>(reader.Value.ToString());
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public static class EnumExtensions
    {
        public static T GetValueFromEnumMember<T>(string value)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(EnumMemberAttribute)) as EnumMemberAttribute;
                if (attribute != null)
                {
                    if (attribute.Value == value)
                        return (T) field.GetValue(null);
                }
                else
                {
                    if (field.Name == value)
                        return (T) field.GetValue(null);
                }
            }

            throw new ArgumentException($"unknow value: {value}");
        }
    }
}