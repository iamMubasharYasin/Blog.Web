using Blog.Web.Models;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        IBlogPostsRepository blogPostsRepository;
        public HomeController(IBlogPostsRepository blogPostsRepository)
        {
            this.blogPostsRepository = blogPostsRepository;
        }
        public async Task<IActionResult> Index()
        {
           var blogPosts =  await blogPostsRepository.GetAllAsync();

            return View(blogPosts);
        }


    }
}
