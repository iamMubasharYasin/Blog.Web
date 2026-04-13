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
            return View("Add");
        }

        public IActionResult ListallTags()
        {
            return View();
        }
        
    }
}
