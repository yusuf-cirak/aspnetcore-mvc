using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ETradeErkanHurnalıP2.Models;

namespace ETradeErkanHurnalıP2.Data
{
    public class ETradeContext : DbContext
    {
        public ETradeContext (DbContextOptions<ETradeContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");
        }

        public DbSet<ETradeErkanHurnalıP2.Models.Product> Products { get; set; }
        public DbSet<ETradeErkanHurnalıP2.Models.Image> Images { get; set; }

    }
}
