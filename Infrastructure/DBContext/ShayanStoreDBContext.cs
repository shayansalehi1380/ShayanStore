using Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DBContext
{
    class ShayanStoreDBContext : IdentityDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<User> Roles { get; set; }
        public DbSet<User> States { get; set; }
        public DbSet<User> Cities { get; set; }

        
    }
}
