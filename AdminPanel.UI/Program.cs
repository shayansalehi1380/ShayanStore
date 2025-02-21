using Microsoft.EntityFrameworkCore;
using Domain.DBContext;

var builder = WebApplication.CreateBuilder(args);

//Connect Postgresql
builder.Services.AddDbContext<ShayanStoreDBContext>(options =>
options.UseNpgsql(builder.Configuration
.GetConnectionString("Host=localhost;Port=5432;Database=ShayanStoreDB;Username=postgres;Password=09011155")));


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
