using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using WORKMAN.Config.Infrastructure.Persistence;

namespace WORKMAN.Config.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseConfigApi(this WebApplication app)
        {
            // Apply migrations
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<ConfigDbContext>();

                dbContext.Database.Migrate();
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
