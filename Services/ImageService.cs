using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;

namespace AfrroStock.Services
{

    public class ImageService : IImageService
    {
        private const string default_Path = "assets";
        private readonly IWebHostEnvironment _env;
        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public bool Create(IFormFile file, out string path)
        {
            path = "";
            if (file != null)
            {
                var fileName = Path.GetFileName(file.FileName);
                path = Path.Combine(default_Path, fileName);
                var absolutePath = Path.Combine(_env.WebRootPath, path);
                using FileStream stream = new FileStream(absolutePath, FileMode.Create);
                file.CopyTo(stream);
                return true;
            }
            return false;
        }

        public bool Edit(IFormFile file, string imageUrl, out string path)
        {
            path = "";
            if (file != null)
            {
                if (imageUrl != null)
                {
                    var oldPath = Path.Combine(_env.WebRootPath, imageUrl);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                var fileName = Path.GetFileName(file.FileName);
                path = Path.Combine(default_Path, fileName);
                var absolutePath = Path.Combine(_env.WebRootPath, path);

                using FileStream stream = new FileStream(absolutePath, FileMode.Create);
                file.CopyTo(stream);
                return true;

            }
            return false;
        }

        public void Delete(string ImageUrl)
        {
            if (string.IsNullOrEmpty(ImageUrl))
            {
                ImageUrl = "";
            }
            var oldPath = Path.Combine(_env.WebRootPath, ImageUrl);
            if (File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
        }

        public string ManipulateImage(IFormFile file)
        {
            string path = null;
            try
            {
                using var image = new MagickImage(file.OpenReadStream());
                image.Resize(image.Width / 2, image.Height / 2);
                image.Quality = 70;

                var logoPath = Path.Combine(default_Path, "afro_logo.png");
                var logoFullPath = Path.Combine(_env.WebRootPath, logoPath);
                using var watermark = new MagickImage(logoFullPath);
                // Optionally make the watermark more transparent
                watermark.Evaluate(Channels.Alpha, EvaluateOperator.Divide, 4);
                // Or draw the watermark at the center
                image.Composite(watermark, Gravity.Center, CompositeOperator.Over);
                var fileName = Path.GetFileName(file.FileName);
                path = Path.Combine(default_Path, $"low_{fileName}");
                var absolutePath = Path.Combine(_env.WebRootPath, path);
                image.Write(absolutePath);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return path;
        }
    }
}