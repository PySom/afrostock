
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AfrroStock.Services
{
    public interface IImageService
    {
        bool Create(IFormFile file, out string path);
        void Delete(string ImageUrl);
        bool Edit(IFormFile file, string imageUrl, out string path);
        ValueTask<(string, string)> ManipulateContent(IFormFile file);
    }
}
