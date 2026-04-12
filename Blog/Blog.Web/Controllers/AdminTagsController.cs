using Blog.Web.Data;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
       private BlogDbContext _blogdbcontext;
        public AdminTagsController(BlogDbContext blogdbcontext)
        {
            _blogdbcontext = blogdbcontext;
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

            _blogdbcontext.tbl_Tags.Add(tag);
            _blogdbcontext.SaveChanges();
            return View("Add");
        }
        
    }
}
