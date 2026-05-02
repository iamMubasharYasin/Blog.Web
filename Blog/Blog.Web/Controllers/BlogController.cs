using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        IBlogPostsRepository blogPostsRepository;
        public BlogController(IBlogPostsRepository blogPostsRepository)
        {
            this.blogPostsRepository = blogPostsRepository;
        }
        [HttpGet]
        public async Task<IActionResult> BlogsDetails (string urlHandle)
        {
            var blogPost = await blogPostsRepository.GetByUrlHandleAsync(urlHandle);
            return View(blogPost);
        }
    }
}
