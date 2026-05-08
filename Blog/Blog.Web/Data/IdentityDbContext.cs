using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data
{
    public class IdentityDbContext
    {
        private DbContextOptions options;

        public IdentityDbContext(DbContextOptions options)
        {
            this.options = options;
        }
    }
}