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
    class ShayanStoreDBContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        public ShayanStoreDBContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("");
            base.OnConfiguring(optionsBuilder);
        }

        
    }
}
