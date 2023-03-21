using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using UsuariosApi_.Models;


namespace UsuariosApi_.Data
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

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName("tbAPI_" + tableName.Substring(6));                    
                }
            }


             CustomIdentityUser admin = new CustomIdentityUser
             {
                 UserName = "admin",
                 NormalizedUserName = "ADMIN",
                 Email = "admin@admin.com",
                 NormalizedEmail = "ADMIN@ADMIN.COM",
                 EmailConfirmed = true,
                 SecurityStamp = Guid.NewGuid().ToString(),
                 Id = 1
             };

             PasswordHasher<CustomIdentityUser> hasher = new PasswordHasher<CustomIdentityUser>();

             admin.PasswordHash = hasher.HashPassword(admin, 
                                 _configuration.GetValue<string>("admininfo:password"));

             builder.Entity<CustomIdentityUser>().HasData(admin);

             builder.Entity<IdentityRole<int>>().HasData(
                 new IdentityRole<int> { Id = 1, Name = "admin", NormalizedName = "ADMIN" }
                 );

             builder.Entity<IdentityRole<int>>().HasData(
                 new IdentityRole<int> { Id = 2, Name = "regular", NormalizedName = "REGULAR" }
                 );

            builder.Entity<IdentityRole<int>>().HasData(
                 new IdentityRole<int> { Id = 3, Name = "developer", NormalizedName = "DEVELOPER" }
                 );

            builder.Entity<IdentityUserRole<int>>().HasData(
                 new IdentityUserRole<int> { RoleId = 1, UserId = 1 }
                 );

        }

    }
}
