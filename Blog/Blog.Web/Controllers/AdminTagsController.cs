using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
       private BlogDbContext blogdbcontext;
        public AdminTagsController(BlogDbContext _blogdbcontext)
        {
            blogdbcontext = _blogdbcontext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async  Task<IActionResult> SubmitTag(AddTagRequest addTagRequest)
        {
            //Mapping the AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await blogdbcontext.tbl_Tags.AddAsync(tag);
            await blogdbcontext.SaveChangesAsync();
            return RedirectToAction("ListallTags");
        }

        public async Task<IActionResult> ListallTags()
        {
            //use dbcontext to read all the tags
            var tags = await blogdbcontext.tbl_Tags.ToListAsync();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            //1st Method 
            // var tags = blogdbcontext.tbl_Tags.Find(id);

            //2nd Method
            var tag = await blogdbcontext.tbl_Tags.FirstOrDefaultAsync(x => x.Id == id);

            if(tag!=null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag = await blogdbcontext.tbl_Tags.FindAsync(tag.Id);

            if(existingTag!=null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                //save changes 
              await  blogdbcontext.SaveChangesAsync();

                //show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });

            }
            //Show error notification
            return RedirectToAction("Edit" , new {id=editTagRequest.Id});
        }

        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
          var tag =  await blogdbcontext.tbl_Tags.FindAsync(editTagRequest.Id);

            if(tag!=null)
            {
                blogdbcontext.tbl_Tags.Remove(tag);
               await blogdbcontext.SaveChangesAsync();

                //Show success notification
                return RedirectToAction("ListallTags");
            }

            // show an error notification
            return RedirectToAction("Edit" ,new {id = editTagRequest.Id});
        }
         
        
    }
}
