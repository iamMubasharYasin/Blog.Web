using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult SubmitTag(AddTagRequest addTagRequest)
        {
            //Mapping the AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            blogdbcontext.tbl_Tags.Add(tag);
            blogdbcontext.SaveChanges();
            return RedirectToAction("ListallTags");
        }

        public IActionResult ListallTags()
        {
            //use dbcontext to read all the tags
            var tags = blogdbcontext.tbl_Tags.ToList();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            //1st Method 
            // var tags = blogdbcontext.tbl_Tags.Find(id);

            //2nd Method
            var tag = blogdbcontext.tbl_Tags.FirstOrDefault(x => x.Id == id);

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
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var existingTag = blogdbcontext.tbl_Tags.Find(tag.Id);

            if(existingTag!=null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                //save changes 
                blogdbcontext.SaveChanges();

                //show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });

            }
            //Show error notification
            return RedirectToAction("Edit" , new {id=editTagRequest.Id});
        }
        
    }
}
