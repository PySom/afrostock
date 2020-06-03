using AfrroStock.Models.ViewModels;
using AfrroStock.Services;
using Microsoft.AspNetCore.Mvc;
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
                    if (_img.Create(model.File, out string path))
                    {
                        var name = model.File.FileName.Split('.')[0];

                        var (lowRes, lowerRes, tags) = await _img.ManipulateContent(model.File);
                        return Ok(
                            new { Content = path, ContentLow = lowRes, ContentLower = lowerRes, 
                            ContentType = contentType, SuggestedTags = tags[0].Select(t => t.Label).Distinct(), Name = name }
                            );
                    }
                    return BadRequest(new { Message = "We could not add this resource. Please try again" });
                }
                return BadRequest(new { Message = "File has to be an image or a video" });
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
    }
}


