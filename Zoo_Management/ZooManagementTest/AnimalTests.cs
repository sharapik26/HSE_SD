using Domain.Entities;
using Domain.ValueObjects;
using Xunit;

public class AnimalTests
{
    [Fact]
    public void Animal_Is_Healthy_By_Default()
    {
        var animal = new Animal("Lion", "Leo", DateTime.UtcNow, Gender.Male, FoodType.Meat, AnimalType.Predator);
        Assert.Equal(AnimalStatus.Healthy, animal.Status);
    }

    [Fact]
    public void Animal_Can_Be_Moved()
    {
        var animal = new Animal("Tiger", "Shere Khan", DateTime.UtcNow, Gender.Male, FoodType.Meat, AnimalType.Predator);
        var newEnclosureId = Guid.NewGuid();

        animal.MoveTo(newEnclosureId);

        Assert.Equal(newEnclosureId, animal.EnclosureId);
    }

    [Fact]
    public void Animal_Can_Be_Healed()
    {
        var animal = new Animal("Parrot", "Kesha", DateTime.UtcNow, Gender.Female, FoodType.Fruits, AnimalType.Herbivore);
        animal.Heal();
        Assert.Equal(AnimalStatus.Healthy, animal.Status);
    }
}
