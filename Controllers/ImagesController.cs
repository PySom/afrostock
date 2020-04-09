using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;
using AfrroStock.Services;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IModelManager<Collection> _collection;
        private readonly IModelManager<Category> _category;
        private readonly IModelManager<Tag> _tag;
        private readonly ImageManager _repo;
        private static readonly MachineLearning _ml;
        public ImagesController(ImageManager repo,
            IModelManager<Collection> collection,
            IModelManager<Category> category,
            IModelManager<Tag> tag)
        {
            (_repo, _collection, _category, _tag) = (repo, collection, category, tag);
            
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
                                                .ToListAsync();
            return Ok(options);

        }

        [HttpGet("{id:int}")]
        public async ValueTask<IActionResult> Get(int id, bool increaseView = false)
        {
            Image model = await _repo
                                .Item()
                                .Where(c => c.Id == id)
                                .Include(c => c.Category)
                                .FirstOrDefaultAsync();
            
            if (model != null)
            {
                if (increaseView)
                {
                    var _ = _repo.IncreaseView(model);
                }
                return Ok(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async ValueTask<IActionResult> Post([FromBody] Image model)
        {
            if (ModelState.IsValid)
            {
                (string collection, string category, string[] tags) = _ml.Pipeline();
                Image addedModel = null;
                //get or set collection
                var categoryId = await CheckCategory(category);
                if (categoryId != 0)
                {
                    model.CategoryId = categoryId;
                    addedModel = model;
                }
                else
                {
                    var collectionId = await CheckCollection(collection);
                    if (collectionId != 0)
                    {
                        categoryId = await CheckCategory(category, collectionId);
                        model.CategoryId = categoryId;
                        addedModel = model;
                    }
                }
                if(addedModel != null)
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
            Image img = new Image { Id = id };
            string message;
            try
            {
                (bool succeeded, string error) = await _repo.Delete(img);
                message = error;
                if (succeeded) return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                message = ex.Message;
            }
            return NotFound(new { Message = message });
        }

        private async ValueTask<int> CheckCollection(string name)
        {
            int id = 0;
            var collection = await _collection.Item().Where(c => c.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
            if(collection != null)
            {
                id = collection.Id;
            }
            else
            {
                (bool succeeded, Collection model, string error) = await _collection.Add(new Collection { Name = name });
                if (succeeded) id = model.Id;
            }
            return id;
        }

        private async ValueTask<int> CheckCategory(string name, int collectionId = 0)
        {
            int id = 0;
            if (collectionId != 0)
            {
                (bool succeeded, Category model, string error) = await _category.Add(new Category { Name = name, CollectionId = collectionId });
                if (succeeded) id = model.Id;
            }
            else
            {
                var category = await _category.Item().Where(c => c.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
                if (category != null)
                {
                    id = category.Id;
                }
            }
            
            return id;
        }

    }
}
