
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using static AfrroStock.Services.MachineLearning;

namespace AfrroStock.Services
{
    public interface IImageService
    {
        bool Create(IFormFile file, out string path);
        void Delete(string ImageUrl);
        bool Edit(IFormFile file, string imageUrl, out string path);
        ValueTask<(string, string, List<List<Predict>>)> ManipulateContent(IFormFile file, string vid);
    }
}
