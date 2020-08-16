﻿using System;
using System.Data;
using Newtonsoft.Json;

namespace XiangJiang.Infrastructure.Serializer.Json
{
    /// <summary>
    ///     DataSetConverter转换器
    /// </summary>
    internal class DataSetConverter : JsonConverter
    {
        /// <summary>
        ///     override CanConvert
        /// </summary>
        /// <param name="valueType">Type</param>
        /// <returns></returns>
        public override bool CanConvert(Type valueType)
        {
            return typeof(DataSet).IsAssignableFrom(valueType);
        }

        /// <summary>
        ///     override ReadJson
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="ser"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            Newtonsoft.Json.JsonSerializer ser)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     override WriteJson
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="dataset"></param>
        /// <param name="ser"></param>
        public override void WriteJson(JsonWriter writer, object dataset, Newtonsoft.Json.JsonSerializer ser)
        {
            var dataSet = dataset as DataSet;
            var converter = new DataTableConverter();
            writer.WriteStartObject();
            writer.WritePropertyName("Tables");
            writer.WriteStartArray();
            if (dataSet != null)
                foreach (DataTable table in dataSet.Tables)
                    converter.WriteJson(writer, table, ser);
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}