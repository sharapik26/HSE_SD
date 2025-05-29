using FileStorageService.Controllers;
using FileStorageService.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismCheckerTests.FileStorage
{
    public class FileUploadControllerTests
    {
        private ReportsDbContext GetTestDbContext()
        {
            var options = new DbContextOptionsBuilder<ReportsDbContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;

            return new ReportsDbContext(options);
        }

        [Fact]
        public async Task UploadFile_SavesFileToDatabase()
        {
            // Arrange
            var context = GetTestDbContext();
            var controller = new FileUploadController(context);

            var content = "Test file content";
            var fileName = "test.txt";

            var file = new FormFile(
                baseStream: new MemoryStream(Encoding.UTF8.GetBytes(content)),
                baseStreamOffset: 0,
                length: content.Length,
                name: "file",
                fileName: fileName
            );

            // Act
            var result = await controller.UploadFile(file);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(await context.Reports.FirstOrDefaultAsync(r => r.FileName == fileName));
        }
    }
}
