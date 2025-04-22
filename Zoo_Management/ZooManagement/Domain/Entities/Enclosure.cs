using Domain.ValueObjects;

namespace Domain.Entities
{

    public class Enclosure
    {
       
            public Guid Id { get; set; }
            public string Name { get; set; } // Новое свойство
            public EnclosureType Type { get; set; }
            public int Size { get; set; }
            public int MaxCapacity { get; set; }
            public List<Guid> AnimalIds { get; set; }

            public Enclosure(string name, EnclosureType type, int size, int maxCapacity) // Обновленный конструктор
            {
                Id = Guid.NewGuid();
                Name = name;
                Type = type;
                Size = size;
                MaxCapacity = maxCapacity;
                AnimalIds = new List<Guid>();
            }

            public void AddAnimal(Guid animalId) => AnimalIds.Add(animalId);
            public void RemoveAnimal(Guid animalId) => AnimalIds.Remove(animalId);
           
        


        public void Clean()
        {
            Console.WriteLine("Enclosure cleaned.");
        }
    }
}
