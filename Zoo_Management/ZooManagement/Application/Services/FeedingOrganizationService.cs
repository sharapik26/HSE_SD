using Application.Interfaces;
using Domain.Events;

namespace Application.Services
{
    public class FeedingOrganizationService
    {
        private readonly IFeedingScheduleRepository _scheduleRepo;
        private readonly IAnimalRepository _animalRepo;

        public FeedingOrganizationService(IFeedingScheduleRepository scheduleRepo, IAnimalRepository animalRepo)
        {
            _scheduleRepo = scheduleRepo;
            _animalRepo = animalRepo;
        }

        public void FeedAnimal(Guid scheduleId)
        {
            var schedule = _scheduleRepo.GetAll().FirstOrDefault(s => s.Id == scheduleId);
            if (schedule == null) throw new Exception("Schedule not found");

            schedule.MarkCompleted();
            _scheduleRepo.Update(schedule);

            var animal = _animalRepo.GetById(schedule.AnimalId);
            animal?.Feed();
            _animalRepo.Update(animal!);

            var feedEvent = new FeedingTimeEvent
            {
                AnimalId = animal.Id,
                ScheduledTime = schedule.FeedingTime
            };

            Console.WriteLine($"FeedingTimeEvent: Animal {feedEvent.AnimalId} was fed at {feedEvent.ScheduledTime}");
        }
    }
}
