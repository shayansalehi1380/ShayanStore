using System.Reflection;
using Domain.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class ConfigureServices
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        var connectionString = "Host=localhost;Port=5432;Database=ShayanStoreDB;Username=postgres;Password=09011155";
        services.AddDbContext<ShayanStoreDBContext>(options =>
            options.UseNpgsql(connectionString));
        return services;
    }
}