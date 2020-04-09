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
    public class CollectionsController : ControllerBase
    {
        private readonly IModelManager<Collection> _repo;
        public CollectionsController(IModelManager<Collection> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async ValueTask<IActionResult> Get()
        {
            ICollection<Collection> options = await _repo
                                                .Item()
                                                .Include(c => c.Categories)
                                                    .ThenInclude(i => i.Images)
                                                .ToListAsync();
            return Ok(options);

        }

        [HttpGet("{id:int}")]
        public async ValueTask<IActionResult> Get(int id)
        {
            Collection model = await _repo
                                .Item()
                                .Where(c => c.Id == id)
                                .Include(c => c.Categories)
                                      .ThenInclude(i => i.Images)
                                .FirstOrDefaultAsync();
            if (model != null)
            {
                return Ok(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async ValueTask<IActionResult> Post([FromBody] Collection model)
        {
            if (ModelState.IsValid)
            {
                (bool succeeded, Collection collection, string error) = await _repo.Add(model);
                if (succeeded) return Ok(collection);
                return BadRequest(new { Message = error });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }


        [HttpPut]
        public async ValueTask<IActionResult> Put([FromBody] Collection model)
        {
            if (ModelState.IsValid)
            {
                (bool succeeded, Collection collection, string error) = await _repo.Update(model);
                if (succeeded) return Ok(collection);
                return BadRequest(new { Message = error });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }

        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> Delete(int id)
        {
            Collection collection = new Collection { Id = id };
            string message;
            try
            {
                (bool succeeded, string error) = await _repo.Delete(collection);
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
