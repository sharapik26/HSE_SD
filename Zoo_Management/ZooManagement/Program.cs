using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ZooManagement.Application;
using ZooManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// ��������� Swagger ��� ������������ API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Zoo API",
        Version = "v1"
    });
});

// ������������ ������� ���������� � ��������������
builder.Services.AddApplication();  // ������� ������-������
builder.Services.AddInfrastructure(); // ����������� � ������� ����������

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // �������� Swagger � ������ ����������
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Zoo API V1");
    });
    app.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger/index.html");
        return Task.CompletedTask;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();  // ������������ �����������

app.Run();
