using System;
using CashRegister.Domain.Authentication;
using CashRegister.Domain.CashManagement;
using CashRegister.Domain.Operations;
using CashRegister.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Infrastructure
{
    public class CashRegisterContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<Detail> OperationDetails { get; set; }

        public DbSet<Registration> CashRegistrations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=cash.db");
        }
    }
}
