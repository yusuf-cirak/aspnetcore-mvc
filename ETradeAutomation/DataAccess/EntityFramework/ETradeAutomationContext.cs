using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CoreMvcOnlineTicariOtomasyon.Models;
using CoreMvcOnlineTicariOtomasyon.Models.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreMvcOnlineTicariOtomasyon.DataAccess.EntityFramework
{
    public class ETradeAutomationContext : DbContext
    {
        public ETradeAutomationContext(DbContextOptions<ETradeAutomationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillPen>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<BillPen>()
                .Property(p => p.QuantityPerUnit)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Expense>()
                .Property(p => p.Cost)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.UnitBuyPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.UnitSellPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SaleMovement>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SaleMovement>()
                .Property(p => p.TotalPrice)
                .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<Bill>()
                .Property(p => p.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Bill>()
                .Property(p => p.Hour)
                .HasColumnType("char(5)");

        }


        public DbSet<Admin> Admins { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillPen> BillPens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Current> Currents { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SaleMovement> SaleMovements { get; set; }
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<CargoDetail> CargoDetails { get; set; }
        public DbSet<CargoTrack> CargoTracks { get; set; }
        public DbSet<Message> Messages { get; set; }


    }
}
