using BuildingBlocks.Common.Contracts.Events;
using BuildingBlocks.Common.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Common.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IEventPublisher, InMemoryEventPublisher>();
            return services;
        }
    }
}
