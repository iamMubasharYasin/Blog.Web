using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
    public class BlogPostsRepository : IBlogPostsRepository
    {
        BlogDbContext blogDbContext;
        public BlogPostsRepository(BlogDbContext _blogDbContext)
        {
            blogDbContext = _blogDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await blogDbContext.AddAsync(blogPost);
            await blogDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingblog = await blogDbContext.tbl_BlogPost.FindAsync(id);
           // throw new NotImplementedException();
           if(existingblog!=null)
            {
                blogDbContext.tbl_BlogPost.Remove(existingblog);
                await blogDbContext.SaveChangesAsync();
                return existingblog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await blogDbContext.tbl_BlogPost.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
           return await blogDbContext.tbl_BlogPost.Include(x=> x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
           var existingBlog =  await blogDbContext.tbl_BlogPost.Include(x=>x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
           

            if(existingBlog!=null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageURL = blogPost.FeaturedImageURL;
                existingBlog.URLHandle = blogPost.URLHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Tags = blogPost.Tags;

                await blogDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }
    }
}
