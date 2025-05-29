using ApiGateway.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq.Protected;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismCheckerTests.ApiGateway
{
    public class GatewayControllerTests
    {
        [Fact]
        public async Task AnalyzeFile_ReturnsOk_WithValidResponse()
        {
            // Поддельный ответ от сервиса анализа
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>(
                   "SendAsync",
                   ItExpr.IsAny<HttpRequestMessage>(),
                   ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("{\"words\": 5}")
               });

            var fakeHttpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://fake-analysis-service")
            };

            // Создаём mock IHttpClientFactory
            var factoryMock = new Mock<IHttpClientFactory>();
            factoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(fakeHttpClient);

            // Передаём mock-фабрику в контроллер
            var controller = new GatewayController(factoryMock.Object);

            // Выполняем метод
            var result = await controller.AnalyzeFile("example.txt");

            // Проверяем результат
            var ok = Assert.IsType<ContentResult>(result);
            Assert.Contains("words", ok.Content);
        }
    }
}
