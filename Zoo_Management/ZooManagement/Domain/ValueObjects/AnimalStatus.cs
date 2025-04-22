namespace Domain.ValueObjects
{
    public record AnimalStatus
    {
        public static readonly AnimalStatus Healthy = new("Healthy");
        public static readonly AnimalStatus Sick = new("Sick");

        public string Value { get; }

        private AnimalStatus(string value) => Value = value;

        public static AnimalStatus FromString(string value) => value switch
        {
            "Healthy" => Healthy,
            "Sick" => Sick,
            _ => throw new ArgumentException("Invalid animal status")
        };

        public override string ToString() => Value;
    }
}