using System.Text.Json;
using System.Text.Json.Serialization;

namespace NSIDictionaryService.Share.Converters
{
    public class CustomDateConverter: JsonConverter<DateOnly>
    {
        private const string DateFormat = "dd.MM.yyyy"; // Matches the format "02.11.2018"

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateAsString = reader.GetString();
            return DateOnly.ParseExact(dateAsString, DateFormat);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormat));
        }
    }
}
