using Microsoft.AspNetCore.Http;

namespace ApiGateway.Models
{
    public class UploadFileDto
    {
        public IFormFile File { get; set; }
    }
}
