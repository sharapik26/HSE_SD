namespace Domain.Events
{
    public class AnimalMovedEvent
    {
        public Guid AnimalId { get; set; }
        public Guid FromEnclosureId { get; set; }
        public Guid ToEnclosureId { get; set; }
    }
}
