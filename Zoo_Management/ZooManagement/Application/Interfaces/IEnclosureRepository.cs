using Domain.Entities;

namespace Application.Interfaces
{
    public interface IEnclosureRepository
    {
        void Add(Enclosure enclosure);
        Enclosure? GetById(Guid id);
        List<Enclosure> GetAll();
        void Update(Enclosure enclosure);
        void Remove(Guid id); // Метод для удаления
    }
}
