using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        IBlogPostsRepository blogPostsRepository;
        IBlogPostLikeRepository blogPostLikeRepository;
        IBlogPostCommentRepository blogPostCommentRepository;
        SignInManager<IdentityUser> signInManager;        

        public BlogController(IBlogPostsRepository blogPostsRepository , 
            IBlogPostLikeRepository blogPostLikeRepository ,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IBlogPostCommentRepository blogPostCommentRepository)
        {
            this.blogPostsRepository = blogPostsRepository;
            this.blogPostLikeRepository = blogPostLikeRepository;
            this.blogPostCommentRepository = blogPostCommentRepository;
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

                //Get Comments for Blog
                var blogCommentsDomainModel = await blogPostCommentRepository.GetByCommentBlogIdAsync(blogPost.Id);
                var blogCommentsforView = new List<BlogCommentViewModel>();
                foreach(var blogComment in blogCommentsDomainModel.OrderByDescending(x => x.DateAdded))
                {
                    var commentUser = await UserManager.FindByIdAsync(blogComment.UserId.ToString());
                    blogCommentsforView.Add(new BlogCommentViewModel
                    {
                            Description = blogComment.Description,
                            DateAdded = blogComment.DateAdded,
                            Username = commentUser?.UserName ?? "User"
                    });
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
                    Liked = liked,
                    Comments = blogCommentsforView
                    
                };
            }
            return View(blogDetailsViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> BlogsDetails(BlogDetailsViewModel blogDetailsViewModel)
        {
            if (!signInManager.IsSignedIn(User) || string.IsNullOrWhiteSpace(blogDetailsViewModel.CommentDescription))
            {
                return RedirectToAction("BlogsDetails", "Blog", new { urlHandle = blogDetailsViewModel.URLHandle });
            }

            var userId = UserManager.GetUserId(User);
            if (!Guid.TryParse(userId, out var parsedUserId))
            {
                return RedirectToAction("BlogsDetails", "Blog", new { urlHandle = blogDetailsViewModel.URLHandle });
            }

            var domainModel = new BlogPostComment
            {
                BlogPostId = blogDetailsViewModel.Id,
                Description = blogDetailsViewModel.CommentDescription,
                UserId = parsedUserId,
                DateAdded = DateTime.Now
            };

           await  blogPostCommentRepository.AddAsync(domainModel);
            return RedirectToAction("BlogsDetails", "Blog", new { urlHandle = blogDetailsViewModel.URLHandle });
        }
    }
}
