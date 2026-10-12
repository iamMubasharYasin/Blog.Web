using Blog.Web.Data;
using Blog.Web.Repositories;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace Blog.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BlogDbContext blogDbContext;
        public BlogPostLikeRepository(BlogDbContext _blogDbContext)
        {
            blogDbContext = _blogDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await blogDbContext.tbl_BlogPostLike.AddAsync(blogPostLike);
            await blogDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await blogDbContext.tbl_BlogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}
