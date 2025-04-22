using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly AnimalTransferService _transferService;

        public TransferController(AnimalTransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpPost]
        public IActionResult MoveAnimal([FromBody] TransferDto dto)
        {
            _transferService.MoveAnimal(dto.AnimalId, dto.ToEnclosureId);
            return Ok("Animal moved.");
        }
    }

    public class TransferDto
    {
        public Guid AnimalId { get; set; }
        public Guid ToEnclosureId { get; set; }
    }
}
