using Microsoft.AspNetCore.Mvc;
using FileAnalysisService.Services;

namespace FileAnalysisService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyzeController : ControllerBase
    {
        [HttpPost]
        public IActionResult AnalyzeText([FromBody] string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return BadRequest("Текст пуст.");

            var result = new
            {
                paragraphs = TextAnalyzer.CountParagraphs(text),
                words = TextAnalyzer.CountWords(text),
                characters = TextAnalyzer.CountCharacters(text)
            };

            return Ok(result);
        }
    }
}
