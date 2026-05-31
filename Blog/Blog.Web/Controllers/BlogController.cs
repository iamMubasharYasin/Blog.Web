using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        IBlogPostsRepository blogPostsRepository;
        IBlogPostLikeRepository blogPostLikeRepository;
        //private int totalLikes;

        public BlogController(IBlogPostsRepository blogPostsRepository , IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostsRepository = blogPostsRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> BlogsDetails (string urlHandle)
        {
            var blogPost = await blogPostsRepository.GetByUrlHandleAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();


            if (blogPost!=null)
            {
               var totalLikes =  await blogPostLikeRepository.GetTotalLikes(blogPost.Id);

                 blogDetailsViewModel = new BlogDetailsViewModel
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    ShortDescription = blogPost.ShortDescription,
                    FeaturedImageURL = blogPost.FeaturedImageURL,
                    URLHandle = blogPost.URLHandle,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author,
                    Visible = blogPost.Visible,
                    Tags = blogPost.Tags,
                    TotalLikes = totalLikes
                };
            }
            return View(blogDetailsViewModel);
        }
    }
}
