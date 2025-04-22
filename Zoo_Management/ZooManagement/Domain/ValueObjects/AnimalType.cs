using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.ValueObjects
{
    [JsonConverter(typeof(AnimalTypeJsonConverter))]
    public record AnimalType
    {
        public static readonly AnimalType Predator = new("Predator");
        public static readonly AnimalType Herbivore = new("Herbivore");
        public static readonly AnimalType Omnivore = new("Omnivore");

        public string Value { get; }

        private AnimalType(string value)
        {
            Value = value;
        }

        public static AnimalType? FromString(string value) =>
            value switch
            {
                "Predator" => Predator,
                "Herbivore" => Herbivore,
                "Omnivore" => Omnivore,
                _ => null // Возвращаем null для некорректных данных
            };

        public override string ToString() => Value;
    }

    public class AnimalTypeJsonConverter : JsonConverter<AnimalType>
    {

        public override AnimalType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            var gender = AnimalType.FromString(value!); ;
            if (gender == null)
            {
                return null;
            }
            return gender;
        }


        public override void Write(Utf8JsonWriter writer, AnimalType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}
