using AfrroStock.Models.ViewModels;
using AfrroStock.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IImageService _img;

        public FilesController(IImageService image)
        {
            _img = image;
        }



        [HttpPost("upload")]
        public async Task<IActionResult> Post([FromForm]FileViewModel model)
        {
            if(ModelState.IsValid && model.File != null)
            {
                var contentType = model.File.ContentType.Split('/')[0];
                if(contentType == "image" || contentType == "video")
                {
                    var data = await ManipulateImageAsync(model.File, contentType);
                    if(data != null)
                    {
                        return Ok(data);
                    }
                    return BadRequest(new { Message = "We could not add this resource. Please try again" });
                }
                return BadRequest(new { Message = "File has to be an image or a video" });
            }
            return BadRequest(new { Message = "Your data is bad" });
        }

        
        [HttpPost("upload/multiple")]
        public async Task<IActionResult> Post([FromForm]ICollection<IFormFile> files)
        {
            if (ModelState.IsValid && files.Count > 0)
            {
                var response = new List<FileResponse>();
                foreach (var file in files)
                {
                    if(file is object)
                    {
                        var contentType = file.ContentType.Split('/')[0];
                        if (contentType == "image" || contentType == "video")
                        {
                            response.Add(await ManipulateImageAsync(file, contentType));
                        }
                    }
                }
                return Ok(response);
            }
            return BadRequest(new { Message = "Your data is bad" });
        }

        [HttpPut("edit")]
        public IActionResult Put([FromForm]FileEditViewModel model)
        {
            if (ModelState.IsValid && model.File != null)
            {
                if (_img.Edit(model.File, model.OldImage, out string path))
                {
                    return Ok(new { Name = path });
                }
                return BadRequest(new { Message = "We could not add this resource. Please try again" });
            }
            return BadRequest(new { Message = "Your data is bad" });
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromBody]FileDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                _img.Delete(model.Image);
                return NoContent();
            }
            return BadRequest(new { Message = "you need to supply an image to remove" });
        }

        private class FileResponse
        {
            public string Content { get; set; }
            public string ContentLow { get; set; }
            public string ContentLower { get; set; }
            public string ContentType { get; set; }
            public string Name { get; set; }
            public List<string> SuggestedTags { get; set; }
        }
        private async ValueTask<FileResponse> ManipulateImageAsync(IFormFile file, string contentType)
        {
            if (_img.Create(file, out string path))
            {
                var name = file.FileName.Split('.')[0];

                var (lowRes, lowerRes, tags) = await _img.ManipulateContent(file, path);

                var t = new List<string>();
                if (tags is object && tags.Count > 0)
                {
                    t = tags[0].Select(r => r.Label).ToList();
                }
                return new FileResponse
                {
                    Content = path,
                    ContentLow = lowRes,
                    ContentLower = lowerRes,
                    ContentType = contentType,
                    SuggestedTags = t,
                    Name = name
                };
            }
            return null;
        }

    }
}


