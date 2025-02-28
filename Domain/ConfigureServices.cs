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
            "Data Source=185.88.152.27,1430;Initial Catalog=ShayanStore;User ID=Shayan;Password=i4qO3j^93;Trust Server Certificate=True";
        services.AddDbContext<ShayanStoreDBContext>(options =>
            options.UseSqlServer(connectionString));
        return services;
    }
}