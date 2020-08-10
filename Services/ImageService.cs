using ImageMagick;
using MediaToolkit;
using MediaToolkit.Core;
using MediaToolkit.Model;
using MediaToolkit.Options;
using MediaToolkit.Services;
using MediaToolkit.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using static AfrroStock.Services.MachineLearning;

namespace AfrroStock.Services
{

    public class ImageService : IImageService
    {
        private const string default_Path = "assets";
        private readonly IWebHostEnvironment _env;
        private readonly IMediaToolkitService _media;
        public ImageService(IWebHostEnvironment env, IMediaToolkitService media)
        {
            _env = env;
            _media = media;
        }
        public bool Create(IFormFile file, out string path)
        {
            path = "";
            if (file != null)
            {
                var ext = Path.GetExtension(file.FileName);
                path = Path.Combine(default_Path, $"{Rename()}{ext}");
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
                var ext = Path.GetExtension(file.FileName);
                path = Path.Combine(default_Path, $"{Rename()}{ext}");
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

        public async ValueTask<(string, string, List<List<Predict>>)> ManipulateContent(IFormFile file, string vid = null)
        {
            string fileType = file.ContentType.Split('/')[0];
            if (fileType == "image")
            {
                return ManipulateImage(file, vid);
            }

            else if (fileType == "video")
            {
                return await ManipulateVideo(vid);
            }
            return (null, null, null);
        }

        private (string, string, List<List<Predict>>) ManipulateImage(IFormFile file, string img)
        {
            string path = null;
            string pathImgLower = null;
            List<List<Predict>> predicted = null;
            try
            {
                using var image = new MagickImage(file.OpenReadStream());
                using var imageLower = new MagickImage(file.OpenReadStream());
                image.Resize(image.Width / 2, image.Height / 2);
                image.Quality = 70;
                imageLower.Resize(imageLower.Width / 2, imageLower.Height / 2);
                imageLower.Quality = 60;

                var logoPath = Path.Combine(default_Path, "afro_logo.png");
                var logoFullPath = Path.Combine(_env.WebRootPath, logoPath);
                using var watermark = new MagickImage(logoFullPath);
                // Draw the watermark at the center
                image.Composite(watermark, Gravity.Center, CompositeOperator.Over);
                var ext = Path.GetExtension(file.FileName);
                path = Path.Combine(default_Path, $"{Rename()}{ext}");
                pathImgLower = Path.Combine(default_Path, $"{Rename()}{ext}");
                var absolutePath = Path.Combine(_env.WebRootPath, path);
                var absolutePathLowerImg = Path.Combine(_env.WebRootPath, pathImgLower);
                image.Write(absolutePath);
                imageLower.Write(absolutePathLowerImg);

                //MachineLearning _ml = new MachineLearning();
                //predicted = _ml.Pipe(img.Split('\\')[1], _env);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return (path, pathImgLower, predicted);
        }

        private async ValueTask<(string, string, List<List<Predict>>)> ManipulateVideo(string file)
        {
            string pathLow = null;
            string pathOverlay = null;
            string pathOverlayTemp = null;
            List<List<Predict>> predicted = null;
            try
            {
                var absolutePathToFile = Path.Combine(_env.WebRootPath, file);

                var fileName = file.Split('\\')[1];
                var fileExt = fileName.Split('.');

                pathLow = Path.Combine(default_Path, $"{Rename()}.{fileExt[1]}");
                var absolutePathNew = Path.Combine(_env.WebRootPath, pathLow);

                pathOverlayTemp = Path.Combine(default_Path, $"{Rename()}.{fileExt[1]}");
                var absolutePathNewOverlayTemp = Path.Combine(_env.WebRootPath, pathOverlayTemp);

                pathOverlay = Path.Combine(default_Path, $"{Rename()}.{fileExt[1]}");
                var absolutePathNewOverlay = Path.Combine(_env.WebRootPath, pathOverlay);

                var pathToThumbnail = $"{Rename()}.jpg";
                var outputForThumbnail = Path.Combine(_env.WebRootPath, default_Path, pathToThumbnail);

                var saveThumbnailTask = new FfTaskSaveThumbnail(
                                                    absolutePathToFile,
                                                    outputForThumbnail,
                                                    TimeSpan.FromSeconds(10)
                                                    );
                await _media.ExecuteAsync(saveThumbnailTask);

                //MachineLearning _ml = new MachineLearning();
                //predicted = _ml.Pipe(outputForThumbnail, _env);


                var getVideoPortionTask = new FfTaskGetVideoPortion(
                                                    absolutePathToFile,
                                                    absolutePathNew,
                                                    TimeSpan.FromSeconds(10)
                                                    );

                var logoPath = Path.Combine(default_Path, "afro_logo.png");
                var logoFullPath = Path.Combine(_env.WebRootPath, logoPath);

                var reduceQualityTask = new FfTaskReduceVideo(
                                                    absolutePathToFile,
                                                    absolutePathNewOverlayTemp
                                                    );

                var addWaterMarkTask = new FfTaskGetOverlay(
                                                    absolutePathNewOverlayTemp,
                                                    absolutePathNewOverlay,
                                                    logoFullPath
                                                    );
                await _media.ExecuteAsync(getVideoPortionTask);
                await _media.ExecuteAsync(reduceQualityTask);
                await _media.ExecuteAsync(addWaterMarkTask);


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            finally
            {
                Delete(pathOverlayTemp);
            }

            return (pathOverlay, pathLow, predicted);
        }

        public string Rename() => Guid.NewGuid().ToString().Replace('-', '_');
    }
}