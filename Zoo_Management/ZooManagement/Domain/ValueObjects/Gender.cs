using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.ValueObjects
{
    [JsonConverter(typeof(GenderJsonConverter))]
    public record Gender
    {
        public static readonly Gender Male = new("Male");
        public static readonly Gender Female = new("Female");

        public string Value { get; }

        private Gender(string value)
        {
            Value = value;
        }

        public static Gender? FromString(string value) =>
            value switch
            {
                "Male" => Male,
                "Female" => Female,
                _ => null // Возвращаем null для некорректных данных
            };

        public override string ToString() => Value;
    }

    public class GenderJsonConverter : JsonConverter<Gender>
    {
        public override Gender? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            var gender = Gender.FromString(value!);
            if (gender == null)
            {
                // Вместо выброса исключения возвращаем null
                return null;
            }
            return gender;
        }


        public override void Write(Utf8JsonWriter writer, Gender value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}
