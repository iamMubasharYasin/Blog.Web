using Blog.Web.Models.Domain;
using Blog.Web.Models.ViewModels;
using Blog.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blog.Web.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        ITagRepository tagRepository;
        IBlogPostsRepository blogPostsRepository;

        public AdminBlogPostsController(ITagRepository _tagRepository , IBlogPostsRepository _blogPostsRepository)
        {
            tagRepository = _tagRepository;
            blogPostsRepository = _blogPostsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();

            var model = new AddBlogPostsRequest
            {
                Tags = tags.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostsRequest addBlogPostsRequest)
        {
            //map view-model to domain model
            var blogPosts = new BlogPost
            {
                Heading = addBlogPostsRequest.Heading,
                PageTitle = addBlogPostsRequest.PageTitle,
                Author = addBlogPostsRequest.Author,
                Content = addBlogPostsRequest.Content,
                ShortDescription = addBlogPostsRequest.ShortDescription,
                FeaturedImageURL = addBlogPostsRequest.FeaturedImageURL,
                PublishedDate = addBlogPostsRequest.PublishedDate,
                URLHandle = addBlogPostsRequest.URLHandle,
                Visible = addBlogPostsRequest.Visible
            };
            //Map Tags from selected Tags
            var selectedTags = new List<Tag>();
            foreach(var selectedTagId in addBlogPostsRequest.SelectedTags)
            {
                var selectedTagIdAsGuid = Guid.Parse(selectedTagId);
                var existingTag = await tagRepository.GetAsync(selectedTagIdAsGuid);

                if(existingTag!=null)
                {
                    selectedTags.Add(existingTag);
                }
            }
            //Mapping tags back to domain model
            blogPosts.Tags = selectedTags;

            await blogPostsRepository.AddAsync(blogPosts);

            return RedirectToAction("Add");

        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
           var blogPosts = await blogPostsRepository.GetAllAsync();
            return View(blogPosts);
        }
    }
}
