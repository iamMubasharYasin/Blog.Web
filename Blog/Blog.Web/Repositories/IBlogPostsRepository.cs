using Blog.Web.Models.Domain;

namespace Blog.Web.Repositories
{
    public interface IBlogPostsRepository
    {
       Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetAsync(Guid id);
        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<BlogPost?> UpdateAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid id);
        
    }
}
