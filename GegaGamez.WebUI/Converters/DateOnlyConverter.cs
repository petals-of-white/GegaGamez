using System.Text.Json;
using System.Text.Json.Serialization;

namespace GegaGamez.WebUI.Converters;

public class DateOnlyConverter : JsonConverter<DateOnly>
{
    private readonly string serializationFormat;
    public DateOnlyConverter() : this(null)
    {

    }

    public DateOnlyConverter(string? serializationFormat)
    {
        serializationFormat = serializationFormat ?? "dd.MM.yyyy";
    }
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return DateOnly.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(serializationFormat));
    }
}
