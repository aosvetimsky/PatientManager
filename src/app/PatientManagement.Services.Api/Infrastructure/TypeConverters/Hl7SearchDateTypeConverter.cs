using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using PatientManagement.Services.Common;

namespace PatientManagement.Services.Api.Infrastructure.TypeConverters
{
    public class Hl7SearchDateJsonConverter : JsonConverter<Hl7SearchDate>
    {
        public override Hl7SearchDate Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Hl7SearchDate.TryCreateFromString(reader.GetString(), out Hl7SearchDate searchDate);
            return searchDate;
        }

        public override void Write(Utf8JsonWriter writer, Hl7SearchDate value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
