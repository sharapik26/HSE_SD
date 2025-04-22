using Domain.Entities;

namespace Application.Interfaces
{
    public interface IFeedingScheduleRepository
    {
        void Add(FeedingSchedule schedule);
        List<FeedingSchedule> GetAll();
        void Update(FeedingSchedule schedule);
    }
}
