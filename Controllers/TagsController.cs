using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly IModelManager<Tag> _repo;
        public TagsController(IModelManager<Tag> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async ValueTask<IActionResult> Get()
        {
            ICollection<Tag> options = await _repo
                                                .Item()
                                                .ToListAsync();
            return Ok(options);

        }

        [HttpGet("{id:int}")]
        public async ValueTask<IActionResult> Get(int id)
        {
            Tag model = await _repo
                                .Item()
                                .Where(c => c.Id == id)
                                .FirstOrDefaultAsync();
            if (model != null)
            {
                return Ok(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async ValueTask<IActionResult> Post([FromBody] Tag model)
        {
            if (ModelState.IsValid)
            {
                (bool succeeded, Tag tag, string error) = await _repo.Add(model);
                if (succeeded) return Ok(tag);
                return BadRequest(new { Message = error });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }


        [HttpPut]
        public async ValueTask<IActionResult> Put([FromBody] Tag model)
        {
            if (ModelState.IsValid)
            {
                (bool succeeded, Tag tag, string error) = await _repo.Update(model);
                if (succeeded) return Ok(tag);
                return BadRequest(new { Message = error });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }

        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> Delete(int id)
        {
            Tag tag = new Tag { Id = id };
            string message;
            try
            {
                (bool succeeded, string error) = await _repo.Delete(tag);
                message = error;
                if (succeeded) return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                message = ex.Message;
            }
            return NotFound(new { Message = message });
        }

    }
}
