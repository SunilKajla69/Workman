using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BuildingBlocks.Common.DependencyInjection;
using WORKMAN.UserManagement.Feature.Users.GetUser;
using WORKMAN.UserManagement.Feature.Users.SearchUsers;
using WORKMAN.UserManagement.Feature.Users.UpdateUser;
using WORKMAN.UserManagement.Feature.Events;

namespace WORKMAN.UserManagement.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUserManagementServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Controllers
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // DbContext
            services.AddDbContext<UserManagementDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("UserManagementDatabase") 
                    ?? configuration.GetConnectionString("DefaultConnection");
                
                // Detect database provider based on connection string
                if (connectionString!.Contains("Data Source=") && connectionString.EndsWith(".db"))
                {
                    // SQLite for local development
                    options.UseSqlite(connectionString);
                }
                else
                {
                    // PostgreSQL for production
                    options.UseNpgsql(connectionString);
                }
            });

            // Feature Handlers (Vertical Slice Pattern)
            services.AddTransient<GetUserHandler>();
            services.AddTransient<SearchUserHandler>();
            services.AddTransient<UpdateUserHandler>();

            // Event Infrastructure (RabbitMQ)
            // Publisher - in case UserManagement needs to publish events
            services.AddRabbitMqEventPublisher();
            
            // Consumer - listens for events from other services
            services.AddRabbitMqEventConsumer();

            // Register Event Handlers
            services.AddTransient<IEventHandler<UserRegisteredEvent>, UserRegisteredEventHandler>();

            // JWT Authentication (validates tokens issued by Auth service)
            services.AddJwtAuthentication(configuration);
            services.AddAuthorization();

            return services;
        }

        private static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwt = configuration.GetSection("Jwt");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwt["Issuer"],
                        ValidAudience = jwt["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwt["SigningKey"]!))
                    };

                    // Add events for debugging
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"JWT Authentication Failed: {context.Exception.Message}");
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine($"JWT Token Validated for user: {context.Principal?.Identity?.Name}");
                            return Task.CompletedTask;
                        },
                        OnMessageReceived = context =>
                        {
                            Console.WriteLine($"JWT Token Received: {context.Token?.Substring(0, Math.Min(50, context.Token?.Length ?? 0))}...");
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}
