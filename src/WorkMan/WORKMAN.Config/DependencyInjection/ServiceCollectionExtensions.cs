using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.RateLimiting;

using System.Reflection;
using System.Threading.RateLimiting;
using WORKMAN.Config.Feature.MenuConfig;
using WORKMAN.Config.Infrastructure.Persistence;
using WORKMAN.Config.Infrastructure.Dapper;
using WORKMAN.Config.Infrastructure.Dapper.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

namespace WORKMAN.Config.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddConfigApplicationServices(
           this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WORKMAN Config API",
                    Version = "v1",
                    Description = "API for WORKMAN Configuration Management"
                });
            });

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
                var connectionString = configuration.GetConnectionString("configDatabase");
                    options.UseNpgsql(connectionString);
            });

            // Register Dapper Infrastructure
            services.AddScoped<IDapperContext, DapperContext>();
            services.AddScoped<IDapperRepository, DapperRepository>();

            // Feature handlers
            //services.AddScoped<FieldTypeHandler>();
            services.AddScoped<MenuConfigHandler>();

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
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });
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
