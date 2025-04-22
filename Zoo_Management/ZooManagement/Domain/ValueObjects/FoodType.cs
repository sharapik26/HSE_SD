using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain.ValueObjects
{
    [JsonConverter(typeof(FoodTypeJsonConverter))]
    public record FoodType
    {
        public static readonly FoodType Meat = new("Meat");
        public static readonly FoodType Grass = new("Grass");
        public static readonly FoodType Fish = new("Fish");
        public static readonly FoodType Fruits = new("Fruits");

        public string Value { get; }

        private FoodType(string value)
        {
            Value = value;
        }

        public static FoodType? FromString(string value) =>
            value switch
            {
                "Meat" => Meat,
                "Grass" => Grass,
                "Fish" => Fish,
                "Fruits" => Fruits,
                _ => null
            };

        public override string ToString() => Value;
    }

    public class FoodTypeJsonConverter : JsonConverter<FoodType>
    {
        
        public override FoodType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            var gender = FoodType.FromString(value!);
            if (gender == null)
            {
                return null;
            }
            return gender;
        }

        public override void Write(Utf8JsonWriter writer, FoodType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}
