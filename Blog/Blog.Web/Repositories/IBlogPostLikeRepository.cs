using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);

        Task<IEnumerable<BlogPostLike>>GetLikesForBlog(Guid blogPostId);

        Task<int> GetTotalLikes(Guid blogPostId);
    }
}