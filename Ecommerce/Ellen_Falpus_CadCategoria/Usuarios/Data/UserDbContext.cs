using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Usuarios.Models;

namespace Usuarios.Data
{
    public class UserDbContext : IdentityDbContext<CustomIdentityUser, IdentityRole<int>, int>
    {
        private IConfiguration _configuration;
    
        public UserDbContext(DbContextOptions<UserDbContext> opt, IConfiguration configuration) : base(opt)
        {
            _configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Usuario>()
            //    .HasIndex(usuario => usuario.Email)
            //    .IsUnique();

            //builder.Entity<Usuario>()
            //    .HasIndex(usuario => usuario.CPF)
            //    .IsUnique();


            CustomIdentityUser admin = new CustomIdentityUser
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Id = 9999
            };

            PasswordHasher<CustomIdentityUser> hasher = new PasswordHasher<CustomIdentityUser>();
            admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");
            builder.Entity<CustomIdentityUser>().HasData(admin);

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>
                {
                    Id = 99999, 
                    Name = "admin",
                    NormalizedName = "ADMIN"
                });

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int>
                {
                    Id = 99998,
                    Name = "lojista",
                    NormalizedName = "LOJISTA"
                });

            builder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> 
                {
                    Id = 99997,
                    Name = "regular",
                    NormalizedName = "REGULAR" 
                });

            builder.Entity<IdentityUserRole<int>>().HasData
                (new IdentityUserRole<int> 
                {
                    RoleId = 99999, 
                    UserId = 9999 
                });


        }
    }
    }

    

