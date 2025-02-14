using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using ZooERP;
using ZooERP.Animals;
using ZooERP.Classes;
using ZooERP.Interfaces;

namespace ZooERP.Tests
{
    public class ZooTests
    {
        [Fact]
        public void AddAnimal_ShouldAddHealthyAnimal()
        {
            var vetClinic = new VetClinic();
            var zoo = new Zoo(vetClinic);
            var monkey = new Monkey(1, 5, 7) { IsHealthy = true };

            zoo.AddAnimal(monkey);

            Assert.Contains(monkey, zoo.GetAnimals());
        }

        [Fact]
        public void AddAnimal_ShouldNotAddUnhealthyAnimal()
        {
            var vetClinic = new VetClinic();
            var zoo = new Zoo(vetClinic);
            var tiger = new Tiger(2, 10) { IsHealthy = false };

            zoo.AddAnimal(tiger);

            Assert.DoesNotContain(tiger, zoo.GetAnimals());
        }
    }

    public class VetClinicTests
    {
        [Fact]
        public void CheckHealth_ShouldReturnTrueForHealthyAnimal()
        {
            var vetClinic = new VetClinic();
            var rabbit = new Rabbit(3, 2, 9) { IsHealthy = true };

            Assert.True(vetClinic.CheckHealth(rabbit));
        }

        [Fact]
        public void CheckHealth_ShouldReturnFalseForUnhealthyAnimal()
        {
            var vetClinic = new VetClinic();
            var wolf = new Wolf(4, 8) { IsHealthy = false };

            Assert.False(vetClinic.CheckHealth(wolf));
        }
    }

    public class AnimalTests
    {
        [Fact]
        public void Animal_ShouldInitializeCorrectly()
        {
            var monkey = new Monkey(5, 6, 8);

            Assert.Equal(5, monkey.Number);
            Assert.Equal(6, monkey.Food);
            Assert.Equal(8, monkey.Kindness);
        }
    }
}
