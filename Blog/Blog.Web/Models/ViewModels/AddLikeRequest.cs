namespace Blog.Web.Models.ViewModels
{
    public class AddLikeRequest
    {
        public Guid BlogPostId { get; set; }
        public int UserId { get; set; }
    }
}
