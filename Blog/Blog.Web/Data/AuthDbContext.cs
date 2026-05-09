using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Role Ids
            var adminRoleId = "221b7fbd-593e-4856-9bfb-a5dcfba257d0";
            var userRoleId = "ff7ee110-0ed6-4127-a551-7fa914bba52f";
            var superAdminRoleId = "e8632dc9-f5d2-4759-81c6-eee98a418415";

            // Seed Roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = adminRoleId
                },

                new IdentityRole
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = userRoleId
                },

                new IdentityRole
                {
                    Id = superAdminRoleId,
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    ConcurrencyStamp = superAdminRoleId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Super Admin User Id
            var superAdminUserId = "6857563a-c93e-469b-a343-e8cf03b31875";

            // IMPORTANT: FIXED PASSWORD HASH (no dynamic hashing)
            var superAdminUser = new IdentityUser
            {
                Id = superAdminUserId,
                UserName = "superadmin@blog.com",
                Email = "superadmin@blog.com",
                NormalizedUserName = "SUPERADMIN@BLOG.COM",
                NormalizedEmail = "SUPERADMIN@BLOG.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAELONG_FIXED_PASSWORD_HASH_VALUE"
            };

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Assign Roles to Super Admin
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    UserId = superAdminUserId,
                    RoleId = adminRoleId
                },

                new IdentityUserRole<string>
                {
                    UserId = superAdminUserId,
                    RoleId = userRoleId
                },

                new IdentityUserRole<string>
                {
                    UserId = superAdminUserId,
                    RoleId = superAdminRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>()
                .HasData(superAdminRoles);
        }
    }
}