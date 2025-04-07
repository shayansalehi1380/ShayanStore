using Domain.DBContext;
using Domain.Entity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop;

public static class ConfigureServices
{
    public static IServiceCollection AddWebAppServices(this IServiceCollection services)
    {
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(3);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        services.AddDistributedMemoryCache();
        services.AddAutoMapper(typeof(Program));

        services.AddIdentity<User, Role>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequiredLength = 4;
                option.SignIn.RequireConfirmedPhoneNumber = false;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(3);
            })
            .AddUserManager<UserManager<User>>()
            .AddEntityFrameworkStores<ShayanStoreDBContext>();

        services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/Admin/AccessDenied";
            options.Cookie.Name = "webappPanel";
            options.ExpireTimeSpan = TimeSpan.FromHours(3);
            options.LoginPath = "/Admin/AdminLogin";
            options.SlidingExpiration = true;
        });
        services.Configure<SecurityStampValidatorOptions>(options =>
        {
            options.ValidationInterval = TimeSpan.FromHours(3); // بررسی کوکی هر 3 ساعت یک بار
        });
        services.AddHttpContextAccessor();
        
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        services.AddEndpointsApiExplorer();
        return services;
    }
}