using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ZooManagement.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Здесь можно регистрировать сервисы Application-слоя
            services.AddScoped<AnimalTransferService>();
            services.AddScoped<FeedingOrganizationService>();
            services.AddScoped<ZooStatisticsService>();

            return services;
        }
    }
}
