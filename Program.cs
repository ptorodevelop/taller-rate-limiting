using API.Quotes.Limit.Data;
using API.Quotes.Limit.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.RateLimiting;
using StackExchange.Redis;
using System.Threading.RateLimiting;

namespace API.Quotes.Limit
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

         
            builder.Services.AddDbContext<QuotesContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Quotes")));

            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
                ConnectionMultiplexer.Connect("localhost:6379")
            );

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IRateLimiterService, RateLimiterService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Use(async (context, next) =>
            {
                var rateLimiterService = context.RequestServices.GetRequiredService<IRateLimiterService>();
                var acquired = await rateLimiterService.AcquireLimit(rateLimiterService._globalLimiter);
                if (!acquired)
                {
                    context.Response.StatusCode = 429;
                    await context.Response.WriteAsync("Global rate limit exceeded. Try again later.");
                    return;
                }
                await next();
            });

            app.Run();
        }
    }
}
