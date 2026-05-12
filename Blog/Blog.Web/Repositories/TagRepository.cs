using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Blog.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        BlogDbContext dbContext;

        public TagRepository(BlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await dbContext.tbl_Tags.AddAsync(tag);
            await dbContext.SaveChangesAsync();
            return tag;
           
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await dbContext.tbl_Tags.FindAsync(id);
           
            if(existingTag!=null)
            {
                dbContext.tbl_Tags.Remove(existingTag);
                await dbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;

        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
           return  await dbContext.tbl_Tags.ToListAsync();  
        }
        public Task<Tag?> GetAsync(Guid id)
        {
            return dbContext.tbl_Tags.FirstOrDefaultAsync(x => x.Id == id);
           // throw new NotImplementedException();
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await dbContext.tbl_Tags.FindAsync(tag.Id);
           
            if(existingTag!=null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await dbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }  
    }
}