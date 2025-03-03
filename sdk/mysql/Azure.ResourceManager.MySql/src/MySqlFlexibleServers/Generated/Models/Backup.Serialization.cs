// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.MySql.FlexibleServers.Models
{
    public partial class Backup : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(BackupRetentionDays))
            {
                writer.WritePropertyName("backupRetentionDays");
                writer.WriteNumberValue(BackupRetentionDays.Value);
            }
            if (Optional.IsDefined(GeoRedundantBackup))
            {
                writer.WritePropertyName("geoRedundantBackup");
                writer.WriteStringValue(GeoRedundantBackup.Value.ToString());
            }
            writer.WriteEndObject();
        }

        internal static Backup DeserializeBackup(JsonElement element)
        {
            Optional<int> backupRetentionDays = default;
            Optional<EnableStatusEnum> geoRedundantBackup = default;
            Optional<DateTimeOffset> earliestRestoreDate = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("backupRetentionDays"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    backupRetentionDays = property.Value.GetInt32();
                    continue;
                }
                if (property.NameEquals("geoRedundantBackup"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    geoRedundantBackup = new EnableStatusEnum(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("earliestRestoreDate"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    earliestRestoreDate = property.Value.GetDateTimeOffset("O");
                    continue;
                }
            }
            return new Backup(Optional.ToNullable(backupRetentionDays), Optional.ToNullable(geoRedundantBackup), Optional.ToNullable(earliestRestoreDate));
        }
    }
}
