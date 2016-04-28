using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HelloEFCore
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HelloDbContext>(o => o.UseSqlite("Filename=./hello.db"));
        }

        public void Configure(IApplicationBuilder app, DbContextOptions<HelloDbContext> dbOptions, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Information);

            SeedDatabase(dbOptions, loggerFactory.CreateLogger("SeedDatabase"));

            app.Run(async context =>
            {
                context.Response.ContentType = "text/plain; charset=UTF-8";

                using (var db = new HelloDbContext(dbOptions))
                {
                    foreach (var greeting in db.Greetings.ToList())
                    {
                        await context.Response.WriteAsync($"Hello in {greeting.Language} is '{greeting.Phrase}'\n");
                    }
                }
            });
        }

        private void SeedDatabase(DbContextOptions<HelloDbContext> dbOptions, ILogger logger)
        {
            using (var db = new HelloDbContext(dbOptions))
            {
                if (!db.Database.EnsureCreated())
                {
                    logger.LogInformation("Database already created");
                    return;
                }

                logger.LogInformation("Begin initializing new database");

                db.AddRange(new[]
                {
                    new Greeting { Language = "English", Phrase = "Hello!" },
                    new Greeting { Language = "Spanish", Phrase = "¡Hola!" },
                    new Greeting { Language = "Italian", Phrase = "Ciao!" },
                });
                
                db.SaveChanges();
            }

            logger.LogInformation("Finished initializing database");
        }
    }
}
