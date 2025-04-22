using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly ZooStatisticsService _stats;

        public StatisticsController(ZooStatisticsService stats)
        {
            _stats = stats;
        }

        [HttpGet]
        public IActionResult GetStatistics()
        {
            return Ok(_stats.GetStatistics());
        }
    }
}
