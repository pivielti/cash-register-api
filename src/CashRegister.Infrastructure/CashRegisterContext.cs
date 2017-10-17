using System;
using System.Linq;
using CashRegister.Domain.Authentication;
using CashRegister.Domain.CashManagement;
using CashRegister.Domain.Operations;
using CashRegister.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace CashRegister.Infrastructure
{
    public class CashRegisterContext : DbContext
    {
        public CashRegisterContext(DbContextOptions<CashRegisterContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Operation> Operations { get; set; }

        public DbSet<Detail> OperationDetails { get; set; }

        public DbSet<Registration> CashRegistrations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>()
            .HasKey(t => new { t.UserId, t.RoleId });

            builder.Entity<UserRole>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pt => pt.UserId);

            builder.Entity<UserRole>()
                .HasOne(pt => pt.Role)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(pt => pt.RoleId);
        }

        public void CreateDefaultAccount(IPasswordService passwordService)
        {
            if (Users.Any(u => u.Login == "administrator"))
                return;
            // create admin role
            var adminRole = new Role()
            {
                Name = "admin"
            };
            Roles.Add(adminRole);

            // create admin user
            var salt = string.Empty;
            var hash = passwordService.HashPassword("password", out salt);
            var adminUser = new User()
            {
                Login = "administrator",
                PasswordHash = hash,
                HashSalt = salt
            };

            // link user and role
            UserRoles.Add(new UserRole
            {
                Role = adminRole,
                User = adminUser
            });

            SaveChanges();
        }
    }
}
