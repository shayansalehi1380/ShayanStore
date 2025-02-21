using Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.DBContext
{
    public class ShayanStoreDBContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public ShayanStoreDBContext(DbContextOptions<ShayanStoreDBContext> options) : base(options)
          {
          }
          public ShayanStoreDBContext()
          {
          }
          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
          {
              optionsBuilder.UseNpgsql(
                  "Host=localhost;Port=5432;Database=ShayanStoreDB;Username=postgres;Password=09011155"
              );
              base.OnConfiguring(optionsBuilder);
          }
    }
}
