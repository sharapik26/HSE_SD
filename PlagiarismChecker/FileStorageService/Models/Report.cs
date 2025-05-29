namespace FileStorageService.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Content { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
