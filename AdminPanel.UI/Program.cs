using Microsoft.EntityFrameworkCore;
using Domain.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Connect to PostgreSQL  
var connectionString = "Host=localhost;Port=5432;Database=ShayanStoreDB;Username=postgres;Password=09011155";
builder.Services.AddDbContext<ShayanStoreDBContext>(options =>
    options.UseNpgsql(connectionString));


// Add services to the container.  
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
