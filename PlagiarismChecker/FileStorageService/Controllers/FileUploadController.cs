using FileStorageService.Data;
using FileStorageService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FileStorageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly ReportsDbContext _db;

        public FileUploadController(ReportsDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не выбран");

            using var reader = new StreamReader(file.OpenReadStream());
            var content = await reader.ReadToEndAsync();

            var report = new Report
            {
                FileName = file.FileName,
                Content = content,
                UploadedAt = DateTime.UtcNow
            };

            _db.Reports.Add(report);
            await _db.SaveChangesAsync();

            return Ok(new { report.Id, report.FileName });
        }

        [HttpGet("{filename}")]
        public async Task<IActionResult> GetFile(string filename)
        {
            var report = await _db.Reports.FirstOrDefaultAsync(r => r.FileName == filename);
            if (report == null)
                return NotFound("Файл не найден.");

            return Ok(report.Content);
        }

        [HttpGet]
        public async Task<IActionResult> ListFiles()
        {
            var files = await _db.Reports
                .Select(r => new { r.Id, r.FileName, r.UploadedAt })
                .ToListAsync();

            return Ok(files);
        }
    }
}
