using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using Infrastructure.Repositories;

namespace ZooManagement.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // In-Memory Repositories
            services.AddSingleton<IAnimalRepository, InMemoryAnimalRepository>();
            services.AddSingleton<IEnclosureRepository, InMemoryEnclosureRepository>();
            services.AddSingleton<IFeedingScheduleRepository, InMemoryFeedingScheduleRepository>();

            return services;
        }
    }
}
