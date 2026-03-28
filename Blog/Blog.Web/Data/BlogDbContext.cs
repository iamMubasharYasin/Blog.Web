using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<BlogPost> tbl_BlogPost { get; set; }

        public DbSet<Tag> tbl_Tags { get; set; }
    }
}
