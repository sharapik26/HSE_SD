using MassTransit;
using Microsoft.EntityFrameworkCore;
using PaymentsService.BackgroundServices;
using PaymentsService.Features;
using PaymentsService.Persistence;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payments API", Version = "v1" });
});


var connectionString = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<PaymentsDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.AddConsumer<OrderPaymentConsumer>();
    busConfigurator.UsingRabbitMq((context, mqConfigurator) =>
    {
        mqConfigurator.Host(builder.Configuration.GetConnectionString("RabbitMQ"), "/", h =>
        {
            h.Username("user");
            h.Password("password");
        });
        mqConfigurator.ReceiveEndpoint("order-created-event", endpointConfigurator =>
        {
            endpointConfigurator.ConfigureConsumer<OrderPaymentConsumer>(context);
        });
    });
});

builder.Services.AddHostedService<OutboxMessageProcessor>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payments API V1");
});

app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PaymentsDbContext>();
    db.Database.Migrate();
}

app.Run();