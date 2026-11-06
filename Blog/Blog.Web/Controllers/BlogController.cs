using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        IBlogPostsRepository blogPostsRepository;
        IBlogPostLikeRepository blogPostLikeRepository;
        SignInManager<IdentityUser> signInManager;        

        public BlogController(IBlogPostsRepository blogPostsRepository , 
            IBlogPostLikeRepository blogPostLikeRepository ,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.blogPostsRepository = blogPostsRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.signInManager = signInManager;
            UserManager = userManager;
            
        }

        public UserManager<IdentityUser> UserManager { get; }

        [HttpGet]
        public async Task<IActionResult> BlogsDetails (string urlHandle)
        {
            var liked = false;
            var blogPost = await blogPostsRepository.GetByUrlHandleAsync(urlHandle);
            var blogDetailsViewModel = new BlogDetailsViewModel();


            if (blogPost!=null)
            {
               var totalLikes =  await blogPostLikeRepository.GetTotalLikes(blogPost.Id);

                if(signInManager.IsSignedIn(User))//get like for this blog for user
                {

                    var alllikesforblogs =  await blogPostLikeRepository.GetLikesForBlog(blogPost.Id);

                    var userId = UserManager.GetUserId(User);

                    if(userId != null)
                    {
                        var likeFromUser = alllikesforblogs.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        liked = likeFromUser != null;
                    }
                    
                }



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
                    TotalLikes = totalLikes,
                    Liked = liked
                };
            }
            return View(blogDetailsViewModel);
        }
    }
}
