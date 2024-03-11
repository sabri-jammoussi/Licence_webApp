using Azure.Core;
using LicenceApp.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LicenceApp.Data
{
    public class LicenceDBContext : DbContext
    {
        public LicenceDBContext(DbContextOptions<LicenceDBContext> options) : base(options)
        {
        }
        public DbSet<UserDao> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ////  CreatePasswordHash(NewUser, out byte[] passwordHash, out byte[] passwordSalt);
            using var hmac = new HMACSHA512();
            builder.Entity<UserDao>().HasData(

            new UserDao
                {
                    Id = 1,
                    FirstName = "sabri",
                    LastName = "jammoussi",
                    Email = "sabrijm123@gmail.com",
                    PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Sabri123")),
                    PasswordSalt = hmac.Key,
                    Role = Enums.Role.Admin
                });
        }

     
    }
}
