using Blog.Web.Models;
using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        IBlogPostsRepository blogPostsRepository;
        ITagRepository tagRepository;
        public HomeController(IBlogPostsRepository blogPostsRepository ,ITagRepository tagRepository)
        {
            this.blogPostsRepository = blogPostsRepository;
            this.tagRepository = tagRepository;
            
        }
        public async Task<IActionResult> Index()
        {
            //getting all blogs
           var blogPosts =  await blogPostsRepository.GetAllAsync();

           var tag =  await tagRepository.GetAllAsync();

            var model = new HomeViewModel
            {
                BlogPosts = blogPosts,
                Tag = tag
            };
            return View(model);
        }


    }
}
