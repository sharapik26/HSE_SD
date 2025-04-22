using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
    {
        private readonly Dictionary<Guid, FeedingSchedule> _schedules = new();

        public void Add(FeedingSchedule schedule)
        {
            _schedules[schedule.Id] = schedule;
        }

        public List<FeedingSchedule> GetAll()
        {
            return _schedules.Values.ToList();
        }

        public void Update(FeedingSchedule schedule)
        {
            _schedules[schedule.Id] = schedule;
        }
    }
}
