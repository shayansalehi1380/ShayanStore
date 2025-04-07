using System.Reflection;
using Domain.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class ConfigureServices
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        var connectionString =
            "Data Source=185.165.118.72,1433;Initial Catalog=sh12opOn;User ID=AdminSa;Password=4p#8V65cb;Trust Server Certificate=True";
        services.AddDbContext<ShayanStoreDBContext>(options =>
            options.UseSqlServer(connectionString));
        return services;
    }
}