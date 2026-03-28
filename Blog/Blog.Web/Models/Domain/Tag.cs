namespace Blog.Web.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }

        //Many-to-Many RelationShip
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
