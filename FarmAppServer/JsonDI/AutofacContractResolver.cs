using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FarmAppServer.JsonDI
{
    public class InterfaceConverter<Resolve, ToInterface> : JsonConverter<ToInterface> where Resolve : class, ToInterface
    {
        public override ToInterface Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<Resolve>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, ToInterface value, JsonSerializerOptions options) { }
    }
}
