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
        public async ValueTask<IActionResult> Get()
        {
            ICollection<Image> options = await _repo
                                                .Item()
                                                .Include(i => i.Author)
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
            var results = await _tag
                                    .Item()
                                    .Where(t => t.Name.ToLower().StartsWith(term.ToLower())
                                                    || t.Name.ToLower().Contains($" {term.ToLower()}"))
                                    .Include(t => t.ImageTags)
                                        .ThenInclude(i => i.Image)
                                            .ThenInclude(i => i.Author)
                                    .Select(t => t.ImageTags.Select(it => it.Image))
                                    .ToListAsync();
            List<Image> images = new List<Image>();
            foreach (var result in results)
            {
                images.AddRange(result);
            }
            return Ok(_mapper.Map<ICollection<Image>, ICollection<ImageDTO>>(images));
        }

        [HttpGet("search")]
        public async ValueTask<IActionResult> Get(string term)
        {
            //search term if image(s) is there
            var results = await _tag
                                    .Item()
                                    .Where(i => i.Name.ToLower().StartsWith(term.ToLower()) 
                                                    || i.Name.ToLower().Contains($" {term.ToLower()}"))
                                    .Include(t => t.ImageTags)
                                        .ThenInclude(i => i.Image)
                                            .ThenInclude(i => i.Author)
                                    .Select(t => t.ImageTags.Select(it => new SearchDTO { Name = it.Image.Name, Id = it.Image.Id }))
                                    .ToListAsync();

            var images = new List<SearchDTO>();
            foreach (var result in results)
            {
                images.AddRange(result);
            }
            return Ok(images);
        }

        [Authorize(Roles = "Author,Both,Super" )]
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
                        if(newTags.Count() > 0)
                        {
                            var (tagSucceeded, tags, tagError) = await _tag.Add(newTags);
                            if (tagSucceeded) checkTags.AddRange(tags);
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
                (bool succeeded, Image img, string error) = await _repo.Update(_mapper.Map<ImageVM, Image>(model));
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
