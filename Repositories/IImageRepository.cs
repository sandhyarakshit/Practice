using System.Net;

namespace SchoolApplication.Repositories
{
    public interface IImageRepository
    {
       Task<string> Upload(IFormFile file, string fileName);
    }
}
