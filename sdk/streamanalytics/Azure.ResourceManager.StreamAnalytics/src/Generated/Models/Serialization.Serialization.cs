// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.StreamAnalytics.Models
{
    public partial class Serialization : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("type");
            writer.WriteStringValue(EventSerializationType.ToString());
            writer.WriteEndObject();
        }

        internal static Serialization DeserializeSerialization(JsonElement element)
        {
            if (element.TryGetProperty("type", out JsonElement discriminator))
            {
                switch (discriminator.GetString())
                {
                    case "Avro": return AvroSerialization.DeserializeAvroSerialization(element);
                    case "Csv": return CsvSerialization.DeserializeCsvSerialization(element);
                    case "CustomClr": return CustomClrSerialization.DeserializeCustomClrSerialization(element);
                    case "Json": return JsonSerialization.DeserializeJsonSerialization(element);
                    case "Parquet": return ParquetSerialization.DeserializeParquetSerialization(element);
                }
            }
            EventSerializationType type = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("type"))
                {
                    type = new EventSerializationType(property.Value.GetString());
                    continue;
                }
            }
            return new Serialization(type);
        }
    }
}
