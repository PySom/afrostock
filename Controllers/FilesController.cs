using AfrroStock.Models.ViewModels;
using AfrroStock.Services;
using Microsoft.AspNetCore.Mvc;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IImageService _img;

        private static readonly MachineLearning _ml;
        public FilesController(IImageService image)
        {
            _img = image;
        }

        static FilesController()
        {
            _ml = new MachineLearning();
        }

        [HttpPost("upload")]
        public IActionResult Post([FromForm]FileViewModel model)
        {
            if(ModelState.IsValid && model.File != null)
            {
                if(_img.Create(model.File, out string path))
                {
                    var name = model.File.FileName.Split('.')[0];
                    var contentType = model.File.ContentType.Split('/')[0];
                    var lowRes = _img.ManipulateImage(model.File);
                    (string _, string[] tags) = _ml.Pipeline();
                    return Ok(new { Content = path, ContentLow = lowRes, ContentType = contentType, SuggestedTags = tags, Name = name });
                }
                return BadRequest(new { Message = "We could not add this resource. Please try again" });
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


