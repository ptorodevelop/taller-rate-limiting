using API.Quotes.Limit.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;


public static class ServiceConnection
{
    public static IServiceCollection AddServiceConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IContext, QuotesDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("QuotesDb")));

        return services;
    }
}
