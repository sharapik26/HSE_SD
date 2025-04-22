using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedingController : ControllerBase
    {
        private readonly IFeedingScheduleRepository _scheduleRepo;
        private readonly IAnimalRepository _animalRepo;

        public FeedingController(IFeedingScheduleRepository scheduleRepo, IAnimalRepository animalRepo)
        {
            _scheduleRepo = scheduleRepo;
            _animalRepo = animalRepo;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_scheduleRepo.GetAll());

        [HttpPost]
        public IActionResult Create([FromBody] FeedingScheduleDto dto)
        {
            var schedule = new FeedingSchedule(dto.AnimalId, dto.FeedingTime, dto.Food);
            _scheduleRepo.Add(schedule);
            return Ok(schedule);
        }

        [HttpPost("complete/{id}")]
        public IActionResult Complete(Guid id)
        {
            var sched = _scheduleRepo.GetAll().FirstOrDefault(s => s.Id == id);
            if (sched == null) return NotFound();

            sched.MarkCompleted();
            _scheduleRepo.Update(sched);

            var animal = _animalRepo.GetById(sched.AnimalId);
            animal?.Feed();

            return Ok(sched);
        }
    }

    public class FeedingScheduleDto
    {
        public Guid AnimalId { get; set; }
        public DateTime FeedingTime { get; set; }
        public FoodType Food { get; set; }
    }
}
