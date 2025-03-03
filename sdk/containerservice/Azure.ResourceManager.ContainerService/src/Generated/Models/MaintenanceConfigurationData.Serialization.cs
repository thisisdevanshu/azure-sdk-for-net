// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;
using Azure.ResourceManager.ContainerService.Models;
using Azure.ResourceManager.Models;

namespace Azure.ResourceManager.ContainerService
{
    public partial class MaintenanceConfigurationData : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("properties");
            writer.WriteStartObject();
            if (Optional.IsCollectionDefined(TimesInWeek))
            {
                writer.WritePropertyName("timeInWeek");
                writer.WriteStartArray();
                foreach (var item in TimesInWeek)
                {
                    writer.WriteObjectValue(item);
                }
                writer.WriteEndArray();
            }
            if (Optional.IsCollectionDefined(NotAllowedTimes))
            {
                writer.WritePropertyName("notAllowedTime");
                writer.WriteStartArray();
                foreach (var item in NotAllowedTimes)
                {
                    writer.WriteObjectValue(item);
                }
                writer.WriteEndArray();
            }
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        internal static MaintenanceConfigurationData DeserializeMaintenanceConfigurationData(JsonElement element)
        {
            ResourceIdentifier id = default;
            string name = default;
            ResourceType type = default;
            Optional<SystemData> systemData = default;
            Optional<IList<ContainerServiceTimeInWeek>> timeInWeek = default;
            Optional<IList<ContainerServiceTimeSpan>> notAllowedTime = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("id"))
                {
                    id = new ResourceIdentifier(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("name"))
                {
                    name = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("type"))
                {
                    type = new ResourceType(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("systemData"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    systemData = JsonSerializer.Deserialize<SystemData>(property.Value.ToString());
                    continue;
                }
                if (property.NameEquals("properties"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        if (property0.NameEquals("timeInWeek"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            List<ContainerServiceTimeInWeek> array = new List<ContainerServiceTimeInWeek>();
                            foreach (var item in property0.Value.EnumerateArray())
                            {
                                array.Add(ContainerServiceTimeInWeek.DeserializeContainerServiceTimeInWeek(item));
                            }
                            timeInWeek = array;
                            continue;
                        }
                        if (property0.NameEquals("notAllowedTime"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            List<ContainerServiceTimeSpan> array = new List<ContainerServiceTimeSpan>();
                            foreach (var item in property0.Value.EnumerateArray())
                            {
                                array.Add(ContainerServiceTimeSpan.DeserializeContainerServiceTimeSpan(item));
                            }
                            notAllowedTime = array;
                            continue;
                        }
                    }
                    continue;
                }
            }
            return new MaintenanceConfigurationData(id, name, type, systemData.Value, Optional.ToList(timeInWeek), Optional.ToList(notAllowedTime));
        }
    }
}
