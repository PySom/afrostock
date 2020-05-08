using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;
using AfrroStock.Services;
using Microsoft.AspNetCore.Authorization;
using AfrroStock.Enums;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IModelManager<Category> _category;
        private readonly IModelManager<UserImage> _userImg;
        private readonly IModelManager<Tag> _tag;
        private readonly ImageManager _repo;
        private static readonly MachineLearning _ml;
        public ImagesController(ImageManager repo,
            IModelManager<Category> category,
            IModelManager<Tag> tag,
            IModelManager<UserImage> userImg)
        {
            (_repo, _category, _tag, _userImg) = (repo, category, tag, userImg);

        }

        static ImagesController()
        {
            _ml = new MachineLearning();
        }

        [HttpGet]
        public async ValueTask<IActionResult> Get()
        {
            ICollection<Image> options = await _repo
                                                .Item()
                                                .Include(c => c.Tags)
                                                .Include(i => i.Author)
                                                .ToListAsync();
            return Ok(options);

        }

        [HttpGet("{id:int}")]
        public async ValueTask<IActionResult> Get(int id)
        {
            Image model = await _repo
                                .Item()
                                .Where(c => c.Id == id)
                                .Include(i => i.Author)
                                .Include(c => c.Category)
                                .FirstOrDefaultAsync();

            if (model != null)
            {
                return Ok(model);
            }
            return NotFound();
        }

        [HttpPut("increaseView/{id:int}")]
        public async ValueTask<IActionResult> IncreaseView(int id)
        {
            var model = _repo
                            .Item()
                            .Where(c => c.Id == id)
                            .FirstOrDefault();
            if (model != null)
            {
                var _ = await _repo.IncreaseView(model);
            }

            return NoContent();
        }

        [HttpGet("videos")]
        public async ValueTask<IActionResult> GetVideos()
        {
            var model = await _repo
                                .Item()
                                .Where(c => c.ContentType == ContentType.Video)
                                .Include(i => i.Author)
                                .Include(c => c.Category)
                                .ToListAsync();

            return Ok(model);
        }

        [HttpGet("videos/searchfor/")]
        public async ValueTask<IActionResult> GetRelatedVideos(string term)
        {
            var results = await _tag
                                    .Item()
                                    .Where(t => t.Name.ToLower().StartsWith(term.ToLower())
                                                    || t.Name.ToLower().Contains($" {term.ToLower()}"))
                                    .Include(t => t.Image)
                                        .ThenInclude(i => i.Author)
                                    .Select(t => t.Image)
                                    .ToListAsync();
            return Ok(results);
        }

        [HttpGet("search")]
        public async ValueTask<IActionResult> Get(string term)
        {
            //search term if image(s) is there
            var results = await _tag
                                    .Item()
                                    .Where(i => i.Name.ToLower().StartsWith(term.ToLower()) 
                                                    || i.Name.ToLower().Contains($" {term.ToLower()}"))
                                    .Select(i => new { i.Name, i.Id })
                                    .ToListAsync();
            return Ok(results);
            


        }

        [Authorize(Roles = "Author,Both,Super" )]
        [HttpPost]
        public async ValueTask<IActionResult> Post([FromBody] Image model)
        {
            if (ModelState.IsValid)
            {
                (string category, string[] tags) = _ml.Pipeline();
                Image addedModel = null;
                //get or set collection
                var categoryId = await CheckCategory(category);
                if (categoryId != 0)
                {
                    model.CategoryId = categoryId;
                    addedModel = model;
                    if (addedModel != null)
                    {
                        (bool succeeded, Image img, string error) = await _repo.Add(addedModel);
                        if (succeeded)
                        {
                            var imageTags = tags.Select(tag => new Tag { Name = tag, ImageId = img.Id });
                            var _ = await _tag.Add(imageTags);
                            return Ok(img);
                        }
                        return BadRequest(new { Message = error });
                    }
                }
                return BadRequest(new { Message = "we could not create the category for this image" });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }


        [HttpPut]
        public async ValueTask<IActionResult> Put([FromBody] Image model)
        {
            if (ModelState.IsValid)
            {
                (bool succeeded, Image img, string error) = await _repo.Update(model);
                if (succeeded) return Ok(img);
                return BadRequest(new { Message = error });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }

        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> Delete(int id)
        {
            bool userHasThisImage = await _userImg.Item().AnyAsync(ui => ui.ImageId == id);
            if (!userHasThisImage)
            {
                Image img = new Image { Id = id };
                string message;
                try
                {
                    (bool succeeded, string error) = await _repo.Delete(img);
                    message = error;
                    if (succeeded) return NoContent();
                    return BadRequest(new { Message = message });

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    message = ex.Message + " " + ex.InnerException.Message;
                }
                return BadRequest(new { Message = message });
            }
            return BadRequest(new { Message = "One or more users have this image. We cannot delete this. Please come up with a policy and notify developer" });
        }


        private async ValueTask<int> CheckCategory(string name)
        {
            int id = 0;
            var category = await _category.Item().Where(c => c.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
            if (category != null)
            {
                id = category.Id;
            }
            else
            {
                (bool succeeded, Category cat, string error) = await _category.Add(new Category{ Name = name });
                if (succeeded) id = cat.Id;
            }
            return id;
        }

    }
}
