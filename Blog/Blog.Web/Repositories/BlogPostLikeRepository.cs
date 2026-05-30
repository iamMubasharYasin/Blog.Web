using Blog.Web.Data;

namespace Blog.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        BlogDbContext blogDbContext;
        public BlogPostLikeRepository(BlogDbContext _blogDbContext)
        {
            blogDbContext = _blogDbContext;
        }

        public Task<int> GetTotalLikes(Guid blogPostId)
        {
            throw new NotImplementedException();
        }
    }
}
