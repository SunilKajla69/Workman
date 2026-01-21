using Microsoft.AspNetCore.HttpOverrides;

namespace WORKMAN.Config.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseConfigApi(this WebApplication app)
        {
            // Apply migrations with retry logic
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WORKMAN Config API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //app.UseGlobalExceptionHandling();

            app.UseCors("config-policy");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.UseRateLimiter();
            return app;
        }
    }
}
