﻿using Domain.Entity.BasicInfo;
using Domain.Entity.BlogPosts;
using Domain.Entity.DiscountCodes;
using Domain.Entity.Products;
using Domain.Entity.Products.Brands;
using Domain.Entity.Products.Categories;
using Domain.Entity.Products.Colors;
using Domain.Entity.Products.Features;
using Domain.Entity.Products.Guaranties;
using Domain.Entity.Products.ImageAttachments;
using Domain.Entity.Products.Offers;
using Domain.Entity.Users;
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
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<FeatureDetails> FeatureDetails { get; set; }
        public DbSet<Guarantee> Guarantees { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ShippingOption> ShippingOptions { get; set; }
        public DbSet<CategoryDetail> CategoryDetails { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ImageGallery> ImageGalleries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Offer> Offers { get; set; }

        public DbSet<BlogPost> BlogPosts { get; set; }

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
                "Data Source=185.165.118.72,1433;Initial Catalog=sh12opOn;User ID=AdminSa;Password=4p#8V65cb;Trust Server Certificate=True"
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
            modelBuilder.Entity<SubCategory>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<Feature>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<FeatureDetails>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<Guarantee>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<Color>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<Brand>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<ShippingOption>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<CategoryDetail>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<Wallet>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<DiscountCode>().HasQueryFilter(x => !x.IsDelete);
            modelBuilder.Entity<BlogPost>().HasQueryFilter(x => !x.IsDelete);

            base.OnModelCreating(modelBuilder);
        }
    }
}