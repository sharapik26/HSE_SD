using Application.Interfaces;
using Domain.Events;

namespace Application.Services
{
    public class AnimalTransferService
    {
        private readonly IAnimalRepository _animalRepo;
        private readonly IEnclosureRepository _enclosureRepo;

        public AnimalTransferService(IAnimalRepository animalRepo, IEnclosureRepository enclosureRepo)
        {
            _animalRepo = animalRepo;
            _enclosureRepo = enclosureRepo;
        }

        public void MoveAnimal(Guid animalId, Guid toEnclosureId)
        {
            var animal = _animalRepo.GetById(animalId);
            if (animal == null) throw new Exception("Animal not found");

            var toEnclosure = _enclosureRepo.GetById(toEnclosureId);
            if (toEnclosure == null) throw new Exception("Enclosure not found");

            toEnclosure.AddAnimal(animalId);

            animal.MoveTo(toEnclosureId);

            _animalRepo.Update(animal);
            _enclosureRepo.Update(toEnclosure);

            var moveEvent = new AnimalMovedEvent
            {
                AnimalId = animalId,
                ToEnclosureId = toEnclosureId
            };

            Console.WriteLine($"AnimalMovedEvent: {moveEvent.AnimalId} moved from {moveEvent.FromEnclosureId} to {moveEvent.ToEnclosureId}");
        }
    }
}
