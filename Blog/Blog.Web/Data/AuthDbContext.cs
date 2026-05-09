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

            // Seed Roles (User, Admin, SuperAdmin)
            var adminRoleId = "1746fb34-b172-4dd1-ab22-869ea568f52b";
            var userRoleId = "5e9105fb-2e9e-4a56-ab35-3da160fceb0c";
            var superAdminRoleId = "83803f48-b25c-4d55-a74c-819a052eeaaf";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // Seed SuperAdmin User
            var superAdminId = "0ac99a87-e692-4ddb-a174-4763f2fb0f36";

            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@blog.com",
                Email = "superadmin@blog.com",
                NormalizedEmail = "SUPERADMIN@BLOG.COM",
                NormalizedUserName = "SUPERADMIN@BLOG.COM",
                Id = superAdminId,
                PasswordHash = "AQAAAAIAAYagAAAAEJok/hx+O2TINdjhFfX8oHBUtVDORdqA0jEd9mRWYiIFQjfevs6exGqTLDtQJ4jwYQ==",
                SecurityStamp = superAdminId,
                ConcurrencyStamp = superAdminId
            };

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Assign Roles to SuperAdmin
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
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}