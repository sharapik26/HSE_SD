using Domain.Entities;
using Domain.ValueObjects;
using Xunit;

public class EnclosureTests
{
    [Fact]
    public void Can_Add_Animal_To_Enclosure()
    {
        var enclosure = new Enclosure("Cage", EnclosureType.Predator, 100, 2);
        var animalId = Guid.NewGuid();

        enclosure.AddAnimal(animalId);

        Assert.Contains(animalId, enclosure.AnimalIds);
    }

    [Fact]
    public void Cannot_Add_Animal_When_Enclosure_Is_Full()
    {
        var enclosure = new Enclosure("Birdcage", EnclosureType.Bird, 50, 1);
        enclosure.AddAnimal(Guid.NewGuid());

        Assert.Throws<InvalidOperationException>(() => enclosure.AddAnimal(Guid.NewGuid()));
    }
}
