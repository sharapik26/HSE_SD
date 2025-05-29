using ApiGateway.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GatewayController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public GatewayController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileDto model)
        {
            var file = model.File;

            if (file == null || file.Length == 0)
                return BadRequest("Файл не выбран");

            try
            {
                using var content = new MultipartFormDataContent();
                using var stream = file.OpenReadStream();
                var fileContent = new StreamContent(stream);
                content.Add(fileContent, "file", file.FileName);

                var response = await _httpClient.PostAsync("http://filestorageservice/api/fileupload", content);

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, "Ошибка при отправке файла в FileStorageService");

                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(502, $"Ошибка проксирования: {ex.Message}");
            }
        }


        // Проксируем запросы на FileStorageService для получения файла
        [HttpGet("file/{filename}")]
        public async Task<IActionResult> GetFile(string filename)
        {
            try
            {
                var response = await _httpClient.GetAsync($"http://filestorageservice/api/fileupload/{filename}");
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Ошибка при получении файла.");
                }

                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            catch (Exception ex)
            {
                return StatusCode(502, $"Ошибка маршрутизации: {ex.Message}");
            }
        }

        // Проксируем запросы на FileAnalysisService для анализа текста
        [HttpPost("analyze/{filename}")]
        public async Task<IActionResult> AnalyzeFile(string filename)
        {
            try
            {
                // Сначала получаем текст из FileStorageService
                var textResponse = await _httpClient.GetAsync($"http://filestorageservice/api/fileupload/{filename}");
                if (!textResponse.IsSuccessStatusCode)
                {
                    return StatusCode((int)textResponse.StatusCode, "Файл не найден.");
                }

                var fileText = await textResponse.Content.ReadAsStringAsync();

                // Отправляем текст на анализ в FileAnalysisService
                var analysisResponse = await _httpClient.PostAsJsonAsync("http://fileanalysisservice/api/analyze", fileText);
                if (!analysisResponse.IsSuccessStatusCode)
                {
                    return StatusCode((int)analysisResponse.StatusCode, "Ошибка анализа текста.");
                }

                var result = await analysisResponse.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(502, $"Ошибка маршрутизации: {ex.Message}");
            }
        }
    }
}
