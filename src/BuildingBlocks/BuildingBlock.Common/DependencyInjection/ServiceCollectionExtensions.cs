using BuildingBlocks.Common.Contracts.Events;
using BuildingBlocks.Common.Infrastructure.Events;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Common.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers RabbitMQ event infrastructure for publishing events
        /// </summary>
        public static IServiceCollection AddRabbitMqEventPublisher(this IServiceCollection services)
        {
            services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();
            return services;
        }

        /// <summary>
        /// Registers RabbitMQ event consumer as a background service
        /// Call this in services that need to consume events (e.g., UserManagement)
        /// </summary>
        public static IServiceCollection AddRabbitMqEventConsumer(this IServiceCollection services)
        {
            services.AddHostedService<RabbitMqEventConsumer>();
            return services;
        }

        /// <summary>
        /// Legacy method - kept for compatibility
        /// Use AddRabbitMqEventPublisher() instead
        /// </summary>
        [Obsolete("Use AddRabbitMqEventPublisher() instead")]
        public static IServiceCollection AddEventInfrastructure(this IServiceCollection services)
        {
            return services.AddRabbitMqEventPublisher();
        }
    }
}

