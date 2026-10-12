using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);

        Task<int> GetTotalLikes(Guid blogPostId);
    }
}
