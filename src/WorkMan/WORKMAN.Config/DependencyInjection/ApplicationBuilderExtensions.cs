using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using WORKMAN.Config.Infrastructure.Persistence;
using WORKMAN.Config.DataSeed;

namespace WORKMAN.Config.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseConfigApi(this WebApplication app)
        {
            // Apply migrations and seed data
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<ConfigDbContext>();
                var logger = scope.ServiceProvider
                    .GetRequiredService<ILogger<ConfigDbContext>>();

                var maxRetries = 5;
                for (int retry = 1; retry <= maxRetries; retry++)
                {
                    try
                    {
                        logger.LogInformation("Attempting to migrate database (attempt {Retry} of {MaxRetries})...", retry, maxRetries);
                        dbContext.Database.Migrate();
                        logger.LogInformation("Database migration completed successfully.");
                        DataSeeder.SeedData(dbContext, logger);
                        break;
                    }
                    catch (Exception ex) when (retry < maxRetries)
                    {
                        logger.LogWarning(ex,
                            "Failed to connect to database (attempt {Retry} of {MaxRetries}). Retrying in {Delay} seconds...",
                            retry, maxRetries, 1);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Failed to migrate database after {MaxRetries} attempts.", maxRetries);
                        throw;
                    }
                }
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WORKMAN Config API v1");
                c.RoutePrefix = string.Empty; // Swagger UI at root
                c.DocumentTitle = "WORKMAN Config API Documentation";
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //app.UseGlobalExceptionHandling();

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.UseRateLimiter();
            return app;
        }
    }
}
