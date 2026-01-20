using BuildingBlocks.Common.DependencyInjection;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.RateLimiting;
using System.Reflection;
using System.Threading.RateLimiting;
using WORKMAN.Config.Feature.FieldTypeConfig;

namespace WORKMAN.Config.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddConfigApplicationServices(
           this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            services.AddDbContext<ConfigDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("ConfigDatabase");
                
                    options.UseNpgsql(connectionString);
                
            });

            // Feature handlers
            services.AddScoped<FieldTypeHandler>();

            services.AddAuthorization();

            //Fluent Validation
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
            services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("config-policy", opt =>
                {
                    opt.Window = TimeSpan.FromSeconds(10);
                    opt.PermitLimit = 10;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.QueueLimit = 0;
                });

                // Custom response when limited
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            //Add Event Infrastructure

            services.AddEventInfrastructure();
            
            // Add Mapster
            services.AddMapster();

            return services;
        }

        private static IServiceCollection AddMapster(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            
            return services;
        }
    }
}
