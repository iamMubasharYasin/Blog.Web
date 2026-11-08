using Blog.Web.Data;
using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        BlogDbContext blogDbContext;
        public BlogPostCommentRepository(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }

        public async Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment)
        {
            await blogDbContext.tbl_BlogPostComment.AddAsync(blogPostComment);
            await blogDbContext.SaveChangesAsync();
            return blogPostComment;  
        }
    }
}
