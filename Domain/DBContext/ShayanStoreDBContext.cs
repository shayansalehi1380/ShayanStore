using Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.DBContext
{
    public class ShayanStoreDBContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }

        public ShayanStoreDBContext(DbContextOptions<ShayanStoreDBContext> options) : base(options)
        {
        }

        public ShayanStoreDBContext()
        {
        }

        //"Host=localhost;Port=5432;Database=ShayanStoreDB;Username=postgres;Password=09011155"
        //"Data Source=185.88.152.27,1430;Initial Catalog=ShayanStore;User ID=Shayan;Password=i4qO3j^93;Trust Server Certificate=True"
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=185.88.152.27,1430;Initial Catalog=ShayanStore;User ID=Shayan;Password=i4qO3j^93;Trust Server Certificate=True"
            );
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<State>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<MainCategory>().HasQueryFilter(x => !x.IsDelete);

            base.OnModelCreating(modelBuilder);
        }
    }
}