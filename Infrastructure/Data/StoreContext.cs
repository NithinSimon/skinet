using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            
        }
        
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductBrands{get;set;}

        public DbSet<ProductType> ProductTypes{get;set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if(Database.ProviderName.Equals("Microsoft.EntityFrameworkCore.Sqlite"))
            {
                foreach(var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(x => x.PropertyType.Equals(typeof(decimal)));
                    foreach(var prop in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(prop.Name).HasConversion<double>();
                    }
                }

            }
        }
    }
}