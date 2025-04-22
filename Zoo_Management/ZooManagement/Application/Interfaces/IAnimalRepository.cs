using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAnimalRepository
    {
        void Add(Animal animal);
        Animal? GetById(Guid id);
        List<Animal> GetAll();
        void Remove(Guid id);
        void Update(Animal animal);
    }
}
