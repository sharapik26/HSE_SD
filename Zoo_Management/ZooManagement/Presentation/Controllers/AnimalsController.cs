using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly IAnimalRepository _animalRepo;

        public AnimalsController(IAnimalRepository animalRepo)
        {
            _animalRepo = animalRepo;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_animalRepo.GetAll());

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var animal = _animalRepo.GetById(id);
            return animal == null ? NotFound() : Ok(animal);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateAnimalDto dto)
        {
            var gender = Gender.FromString(dto.Gender);
            var favoriteFood = FoodType.FromString(dto.FavoriteFood);
            var type = AnimalType.FromString(dto.Type);

            if (gender == null || favoriteFood == null || type == null)
            {
                return BadRequest(new { message = "Некорректные данные. Проверьте введенные значения для пола, типа пищи или типа животного." });
            }

            var animal = new Animal(dto.Species, dto.Name, dto.BirthDate, gender, favoriteFood, type);
            _animalRepo.Add(animal);
            return CreatedAtAction(nameof(Get), new { id = animal.Id }, animal);
        }



        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _animalRepo.Remove(id);
            return NoContent();
        }
    }

    public class CreateAnimalDto
    {
        public string Species { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } // Теперь строка
        public string FavoriteFood { get; set; } // Теперь строка
        public string Type { get; set; } // Теперь строка
    }

}
