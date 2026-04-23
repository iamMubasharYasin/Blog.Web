using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        ITagRepository tagRepository;

        public AdminBlogPostsController(ITagRepository _tagRepository)
        {
            tagRepository = _tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //get all tags from the repo
            var tags = await tagRepository.GetAllAsync();

            var model = new AddBlogPostsRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString()})
            };

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddBlogPostsRequest addBlogPostsRequest)
        {
            return RedirectToAction("Add");
        }
    }
}
