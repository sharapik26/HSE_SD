using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class InMemoryEnclosureRepository : IEnclosureRepository
    {
        private readonly Dictionary<Guid, Enclosure> _enclosures = new();

        public void Add(Enclosure enclosure)
        {
            _enclosures[enclosure.Id] = enclosure;
        }

        public Enclosure? GetById(Guid id)
        {
            _enclosures.TryGetValue(id, out var enclosure);
            return enclosure;
        }

        public List<Enclosure> GetAll()
        {
            return _enclosures.Values.ToList();
        }

        public void Update(Enclosure enclosure)
        {
            _enclosures[enclosure.Id] = enclosure;
        }
        public void Remove(Guid id) => _enclosures.Remove(id); // Реализация удаления
    }
}
