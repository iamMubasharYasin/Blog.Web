using Blog.Web.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        [HttpPost]
        [Route("Add")]

        public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest)
        {
            return Ok("Add Like endpoint is working.");
        }
    }
}
