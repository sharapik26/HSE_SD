using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class InMemoryAnimalRepository : IAnimalRepository
    {
        private readonly Dictionary<Guid, Animal> _animals = new();

        public void Add(Animal animal)
        {
            _animals[animal.Id] = animal;
        }

        public Animal? GetById(Guid id)
        {
            _animals.TryGetValue(id, out var animal);
            return animal;
        }

        public List<Animal> GetAll()
        {
            return _animals.Values.ToList();
        }

        public void Remove(Guid id)
        {
            _animals.Remove(id);
        }

        public void Update(Animal animal)
        {
            _animals[animal.Id] = animal;
        }
    }
}
