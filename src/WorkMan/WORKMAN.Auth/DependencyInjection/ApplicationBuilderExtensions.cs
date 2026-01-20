using Microsoft.AspNetCore.HttpOverrides;

namespace WORKMAN.Auth.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseAuthApi(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<AuthDbContext>();
                var logger = scope.ServiceProvider
                    .GetRequiredService<ILogger<AuthDbContext>>();

                // Retry logic for database migration with exponential backoff
                var maxRetries = 5;
                for (int retry = 1; retry <= maxRetries; retry++)
                {
                    try
                    {
                        logger.LogInformation("Attempting to migrate database (attempt {Retry} of {MaxRetries})...", retry, maxRetries);
                        dbContext.Database.Migrate();
                        logger.LogInformation("Database migration completed successfully.");
                        break;
                    }
                    catch (Exception ex) when (retry < maxRetries)
                    {
                        var delay = TimeSpan.FromSeconds(Math.Pow(2, retry));
                        logger.LogWarning(ex,
                            "Failed to connect to database (attempt {Retry} of {MaxRetries}). Retrying in {Delay} seconds...",
                            retry, maxRetries, delay.TotalSeconds);
                        Thread.Sleep(delay);
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
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "WORKMAN Auth API V2");
                c.RoutePrefix = string.Empty;
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseGlobalExceptionHandling();

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
