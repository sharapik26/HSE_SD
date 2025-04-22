using Application.Interfaces;

namespace Application.Services
{
    public class ZooStatisticsService
    {
        private readonly IAnimalRepository _animalRepo;
        private readonly IEnclosureRepository _enclosureRepo;

        public ZooStatisticsService(IAnimalRepository animalRepo, IEnclosureRepository enclosureRepo)
        {
            _animalRepo = animalRepo;
            _enclosureRepo = enclosureRepo;
        }

        public object GetStatistics()
        {
            var totalAnimals = _animalRepo.GetAll().Count;
            var enclosures = _enclosureRepo.GetAll();
            var freeEnclosures = enclosures.Count(e => e.AnimalIds.Count == 0);
            var totalEnclosures = enclosures.Count;

            return new
            {
                TotalAnimals = totalAnimals,
                TotalEnclosures = totalEnclosures,
                FreeEnclosures = freeEnclosures
            };
        }
    }
}
