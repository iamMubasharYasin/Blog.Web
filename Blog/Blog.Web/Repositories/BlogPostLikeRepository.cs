using Blog.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        BlogDbContext blogDbContext;
        public BlogPostLikeRepository(BlogDbContext _blogDbContext)
        {
            blogDbContext = _blogDbContext;
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {
            return await blogDbContext.tbl_BlogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
            //throw new NotImplementedException();
        }
    }
}
