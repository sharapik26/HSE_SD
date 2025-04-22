using Domain.ValueObjects;

namespace Domain.Entities
{
    public class FeedingSchedule
    {
        public Guid Id { get; private set; }
        public Guid AnimalId { get; private set; }
        public DateTime FeedingTime { get; private set; }
        public FoodType Food { get; private set; }
        public bool IsCompleted { get; private set; }

        public FeedingSchedule(Guid animalId, DateTime feedingTime, FoodType food)
        {
            Id = Guid.NewGuid();
            AnimalId = animalId;
            FeedingTime = feedingTime;
            Food = food;
            IsCompleted = false;
        }

        public void UpdateSchedule(DateTime newTime)
        {
            FeedingTime = newTime;
        }

        public void MarkCompleted()
        {
            IsCompleted = true;
        }
    }
}
