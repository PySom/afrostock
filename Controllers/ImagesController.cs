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
using Microsoft.EntityFrameworkCore.Internal;
using AfrroStock.Models.DTOs;
using AfrroStock.Models.ViewModels;
using System;
using Microsoft.AspNetCore.JsonPatch;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IModelManager<ImageTag> _imgTag;
        private readonly IModelManager<UserImage> _userImg;
        private readonly IModelManager<Tag> _tag;
        private readonly ImageManager _repo;
        private readonly IMapper _mapper;
        public ImagesController(ImageManager repo,
            IModelManager<Tag> tag,
            IModelManager<UserImage> userImg,
            IModelManager<ImageTag> imgTag,
            IMapper mapper)
        {
            (_repo, _tag, _userImg, _imgTag, _mapper) = (repo, tag, userImg, imgTag, mapper);

        }



        [HttpGet]
        public async ValueTask<IActionResult> GetAll(int page = 1)
        {
            int itemsPerPage = 20;
            ICollection<Image> options = await _repo
                                                .Item()
                                                .Include(i => i.Author)
                                                .Skip(page * itemsPerPage - itemsPerPage)
                                                .Take(itemsPerPage)
                                                .ToListAsync();
            return Ok(_mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(options));

        }

        [HttpGet("{id:int}")]
        public async ValueTask<IActionResult> Get(int id)
        {
            Image model = await _repo
                                .Item()
                                .Where(c => c.Id == id)
                                .Include(i => i.Author)
                                .FirstOrDefaultAsync();

            if (model != null)
            {
                return Ok(_mapper.Map<Image, ImageDTO>(model));
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
                                .ToListAsync();

            return Ok(_mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(model));
        }

        [HttpGet("videos/searchfor/")]
        public async ValueTask<IActionResult> GetRelatedVideos(string term)
        {
            var tags = await _tag
                                .Item()
                                .Where(i => i.Name.ToLower().StartsWith(term.ToLower())
                                                || i.Name.ToLower().Contains($" {term.ToLower()}"))
                                .Include(t => t.ImageTags)
                                        .ThenInclude(i => i.Image)
                                            .ThenInclude(i => i.Author)
                                 .Select(t => t.ImageTags.Select(it => it.Image))
                                .ToListAsync();

            var images = await _repo
                                .Item()
                                .Where(i => i.Name.ToLower().StartsWith(term.ToLower())
                                                || i.Name.ToLower().Contains($" {term.ToLower()}"))
                                .Include(i => i.Author)
                                .ToListAsync();

            List<Image> aggregateTagImages = new List<Image>();
            foreach (var result in tags)
            {
                aggregateTagImages.AddRange(result);
            }
            aggregateTagImages.AddRange(images);
            return Ok(_mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(aggregateTagImages));
        }

        [HttpGet("search")]
        public async ValueTask<IActionResult> Get(string term)
        {
            //search term if image(s) is there
            var tags = await _tag
                                .Item()
                                .Where(i => i.Name.ToLower().StartsWith(term.ToLower())
                                                || i.Name.ToLower().Contains($" {term.ToLower()}"))
                                .Select(t => new SearchDTO { Name = t.Name, Id = t.Id })
                                .ToListAsync();

            var images = await _repo
                                .Item()
                                .Where(i => i.Name.ToLower().StartsWith(term.ToLower())
                                                || i.Name.ToLower().Contains($" {term.ToLower()}"))
                                .Select(t => new SearchDTO { Name = t.Name, Id = t.Id })
                                .ToListAsync();

            images.AddRange(tags);
            return Ok(images);
        }

        [Authorize(Roles = "Author,Both,Super")]
        [HttpPost]
        public async ValueTask<IActionResult> Post([FromBody] ImageVM modelVm)
        {
            if (ModelState.IsValid)
            {
                var model = _mapper.Map<ImageVM, Image>(modelVm);
                (bool succeeded, Image img, string error) = await _repo.Add(model);
                if (succeeded)
                {
                    if (modelVm.SuggestedTags != null && modelVm.SuggestedTags.Length > 0)
                    {
                        var stags = modelVm.SuggestedTags.Select(st => st.ToLower());
                        var checkTags = _tag.Item().Where(t => stags.Contains(t.Name.ToLower())).Select(i => i).ToList();
                        var noTags = modelVm.SuggestedTags.Where(st => !checkTags.Any(ct => ct.Name.ToLower() == st.ToLower()));
                        var newTags = noTags.Select(tag => new Tag { Name = tag });
                        if (newTags.Count() > 0)
                        {
                            var (tagSucceeded, _, tagError) = await _tag.Add(newTags);
                            if (tagSucceeded)
                            {
                                var newTagNames = newTags.Select(n => n.Name.ToLower());
                                var tags = await _tag.Item().Where(n => newTagNames.Contains(n.Name.ToLower())).ToListAsync();
                                checkTags.AddRange(tags);
                            }
                        }
                        var imageTags = checkTags.Select(ct => new ImageTag { ImageId = img.Id, TagId = ct.Id });
                        var _ = await _imgTag.Add(imageTags);
                    }
                    return Ok(img);
                }
                return BadRequest(new { Message = error });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }


        [HttpPut]
        public async ValueTask<IActionResult> Put([FromBody] ImageVM model)
        {
            if (ModelState.IsValid)
            {
                var image = await _repo.Item().FindAsync(model.Id);
                if (image != null)
                {
                    var modelToUpdate = _mapper.Map(model, image);
                    (bool succeeded, Image img, string error) = await _repo.Update(modelToUpdate);
                    if (succeeded) return Ok(img);
                    return BadRequest(new { Message = error });
                }
                return BadRequest(new { Message = "You sure you got this image???" });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }


        [HttpPatch("{id:int}")]
        public async ValueTask<IActionResult> Put([FromBody]JsonPatchDocument<Image> patchDoc, int id)
        {
            var model = await _repo.Item().FindAsync(id);
            if (model != null)
            {
                patchDoc.ApplyTo(model, ModelState);
                if (ModelState.IsValid)
                {
                    (bool succeeded, Image img, string error) = await _repo.Update(model);
                    if (succeeded) return Ok(img);
                    return BadRequest(new { Message = error });
                }
                return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
            }
            return BadRequest(new { Message = "No such item" });
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


        //private async ValueTask<int> CheckCategory(string name)
        //{
        //    int id = 0;
        //    var category = await _category.Item().Where(c => c.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        //    if (category != null)
        //    {
        //        id = category.Id;
        //    }
        //    else
        //    {
        //        (bool succeeded, Category cat, string error) = await _category.Add(new Category{ Name = name });
        //        if (succeeded) id = cat.Id;
        //    }
        //    return id;
        //}

    }
}
