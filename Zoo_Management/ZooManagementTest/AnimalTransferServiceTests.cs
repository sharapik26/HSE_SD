using Application.Services;
using Domain.Entities;
using Domain.ValueObjects;
using Moq;
using Xunit;
using Application.Interfaces;

public class AnimalTransferServiceTests
{
    [Fact]
    public void Moves_Animal_Between_Enclosures()
    {
        var animalId = Guid.NewGuid();
        var fromId = Guid.NewGuid();
        var toId = Guid.NewGuid();

        var animal = new Animal("Elephant", "Dumbo", DateTime.UtcNow, Gender.Male, FoodType.Fruits, AnimalType.Predator);
        animal.MoveTo(fromId);

        var fromEnc = new Enclosure("Cage", EnclosureType.Herbivore, 100, 10);
        fromEnc.AddAnimal(animalId);

        var toEnc = new Enclosure("Cage", EnclosureType.Herbivore, 100, 10);

        var mockAnimalRepo = new Mock<IAnimalRepository>();
        var mockEnclosureRepo = new Mock<IEnclosureRepository>();

        mockAnimalRepo.Setup(r => r.GetById(animalId)).Returns(animal);
        mockEnclosureRepo.Setup(r => r.GetById(fromId)).Returns(fromEnc);
        mockEnclosureRepo.Setup(r => r.GetById(toId)).Returns(toEnc);

        var service = new AnimalTransferService(mockAnimalRepo.Object, mockEnclosureRepo.Object);

        service.MoveAnimal(animalId, toId);

        Assert.Contains(animalId, toEnc.AnimalIds);
        Assert.DoesNotContain(animalId, fromEnc.AnimalIds);
        Assert.Equal(toId, animal.EnclosureId);
    }
}
