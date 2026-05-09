using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed (User,Admin,SuperAdmin)
            var adminRoleId = "221b7fbd-593e-4856-9bfb-a5dcfba257d0";
            var userRoleId = "ff7ee110-0ed6-4127-a551-7fa914bba52f";
            var superadminRoleId = "e8632dc9-f5d2-4759-81c6-eee98a418415";

            var roles = new List<IdentityRole>
            {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "Admin",
                Id = adminRoleId,
                ConcurrencyStamp = adminRoleId
            },

             new IdentityRole
            {
                Name = "User",
                NormalizedName = "User",
                Id = userRoleId,
                ConcurrencyStamp = userRoleId
            },

             new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Id = userRoleId,
                ConcurrencyStamp = userRoleId
            },

        };

            builder.Entity<IdentityRole>().HasData(roles);

            //Seed SuperAdmin
            var superAdminId = "6857563a-c93e-469b-a343-e8cf03b31875";
            var SuperAdminUser = new IdentityUser
            {
                UserName = "superadmin@blog.com",
                Email = "superadmin@blog.com",
                NormalizedEmail = "superadmin@blog.com".ToUpper(),
                NormalizedUserName = "superadmin@blog.com".ToUpper(),
                Id = superAdminId
            };

            SuperAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(SuperAdminUser, "Superadmin@123");
             builder.Entity<IdentityRole>().HasData(SuperAdminUser);

            //Mapping SuperAdmin to User & Admin
            //Add All roles to SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superadminRoleId,
                    UserId = superAdminId
                },
            };

            builder.Entity<IdentityUser>().HasData(SuperAdminUser);
           
        }

       
    }
}