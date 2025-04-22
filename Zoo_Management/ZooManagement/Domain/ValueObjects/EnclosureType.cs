namespace Domain.ValueObjects
{
    public record EnclosureType
    {
        public static readonly EnclosureType Predator = new("Predator");
        public static readonly EnclosureType Herbivore = new("Herbivore");
        public static readonly EnclosureType Bird = new("Bird");
        public static readonly EnclosureType Aquarium = new("Aquarium");

        public string Value { get; }

        private EnclosureType(string value)
        {
            Value = value;
        }

        public static EnclosureType? FromString(string value) =>
    value switch
    {
        "Predator" => Predator,
        "Herbivore" => Herbivore,
        "Bird" => Bird,
        "Aquarium" => Aquarium,
        _ => null // Возвращаем null для некорректных данных
    };


        public override string ToString() => Value;
    }
}
