namespace Domain.Events
{
    public class FeedingTimeEvent
    {
        public Guid AnimalId { get; set; }
        public DateTime ScheduledTime { get; set; }
    }
}
