using Domain.ValueObjects;

namespace Domain.Entities
{

    public class Animal
    {
        public Guid Id { get; set; }
        public string Species { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public FoodType FavoriteFood { get; set; }
        public AnimalStatus Status { get; set; }
        public Guid EnclosureId { get; set; }
        public AnimalType Type { get; set; }

        public Animal(string species, string name, DateTime birthDate, Gender gender, FoodType favoriteFood, AnimalType type)
        {
            Id = Guid.NewGuid();
            Species = species;
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            FavoriteFood = favoriteFood;
            Status = AnimalStatus.Healthy;
            Type = type;
        }

        public void Feed()
        {
            Console.WriteLine($"{Name} has been fed.");
        }

        public void Heal()
        {
            Status = AnimalStatus.Healthy;
        }

        public void MoveTo(Guid enclosureId)
        {
            EnclosureId = enclosureId;
        }
    }
}
