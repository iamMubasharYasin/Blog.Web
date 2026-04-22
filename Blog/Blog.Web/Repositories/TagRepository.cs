using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext dbContext;

        public TagRepository(BlogDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL
        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await dbContext.tbl_Tags.ToListAsync();
        }

        // GET BY ID
        public async Task<Tag?> GetByIdAsync(Guid id)
        {
            return await dbContext.tbl_Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        // ADD
        public async Task<Tag> AddAsync(Tag tag)
        {
            await dbContext.tbl_Tags.AddAsync(tag);
            await dbContext.SaveChangesAsync();
            return tag;
        }

        // UPDATE
        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await dbContext.tbl_Tags.FirstOrDefaultAsync(x => x.Id == tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await dbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }

        // DELETE
        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await dbContext.tbl_Tags.FirstOrDefaultAsync(x => x.Id == id);

            if (existingTag != null)
            {
                dbContext.tbl_Tags.Remove(existingTag);
                await dbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }
    }
}