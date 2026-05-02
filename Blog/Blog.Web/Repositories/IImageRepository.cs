namespace Blog.Web.Repositories
{

    // Repository for ImageUpload
    public interface IImageRepository
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
