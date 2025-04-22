using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EnclosuresController : ControllerBase
    {
        private readonly IEnclosureRepository _enclosureRepo;

        public EnclosuresController(IEnclosureRepository enclosureRepo)
        {
            _enclosureRepo = enclosureRepo;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateEnclosureDto dto)
        {
            var type = EnclosureType.FromString(dto.Type);

            if (type == null)
            {
                return BadRequest(new { message = "Некорректные данные. Проверьте введенное значение для типа вольера." });
            }

            var enclosure = new Enclosure(dto.Name, type, dto.Size, dto.MaxCapacity);
            _enclosureRepo.Add(enclosure);
            return CreatedAtAction(nameof(Get), new { id = enclosure.Id }, enclosure);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var enclosure = _enclosureRepo.GetById(id);
            return enclosure == null ? NotFound() : Ok(enclosure);
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_enclosureRepo.GetAll());

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _enclosureRepo.Remove(id);
            return NoContent();
        }
    }


    public class CreateEnclosureDto
    {
        public string Name { get; set; }
        public string Type { get; set; } // Теперь строка
        public int Size { get; set; }
        public int MaxCapacity { get; set; }
    }

}
