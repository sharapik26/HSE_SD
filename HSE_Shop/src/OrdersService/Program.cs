using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrdersService.BackgroundServices;
using OrdersService.Features;
using OrdersService.Persistence;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orders API", Version = "v1" });
});

var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<OrdersDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.AddConsumer<PaymentResultConsumer>();
    busConfigurator.UsingRabbitMq((context, mqConfigurator) =>
    {
        mqConfigurator.Host(builder.Configuration.GetConnectionString("RabbitMQ"), "/", h =>
        {
            h.Username("user");
            h.Password("password");
        });
        mqConfigurator.ReceiveEndpoint("payment-result-event", endpointConfigurator =>
        {
            endpointConfigurator.Durable = true;
            endpointConfigurator.ConfigureConsumer<PaymentResultConsumer>(context);
        });
    });
});

builder.Services.AddHostedService<OutboxMessageProcessor>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders API V1");
});

app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrdersDbContext>();
    db.Database.Migrate();
}

app.Run();