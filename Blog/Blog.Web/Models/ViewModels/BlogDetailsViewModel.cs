using Blog.Web.Models.Domain;

namespace Blog.Web.Models.ViewModels
{
    public class BlogDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageURL { get; set; }
        public string URLHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public Boolean Visible { get; set; }
        public int TotalLikes { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
