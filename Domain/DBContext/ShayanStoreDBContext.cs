using Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DBContext
{
    public class ShayanStoreDBContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        public ShayanStoreDBContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ShayanStoreDB;Username=postgres;Password=09011155");
            base.OnConfiguring(optionsBuilder);
        }

        
    }
}
