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
    public class CategoriesController : ControllerBase
    {
        private readonly IModelManager<Category> _repo;
        public CategoriesController(IModelManager<Category> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async ValueTask<IActionResult> Get()
        {
            ICollection<Category> options = await _repo
                                                .Item()
                                                .Include(c => c.Images)
                                                    .ThenInclude(i => i.Tags)
                                                .ToListAsync();
            return Ok(options);

        }

        [HttpGet("{id:int}")]
        public async ValueTask<IActionResult> Get(int id)
        {
            Category model = await _repo
                                .Item()
                                .Where(c => c.Id == id)
                                .Include(c => c.Images)
                                    .ThenInclude(i => i.Tags)
                                .FirstOrDefaultAsync();
            if (model != null)
            {
                return Ok(model);
            }
            return NotFound();
        }

        [HttpPost]
        public async ValueTask<IActionResult> Post([FromBody] Category model)
        {
            if (ModelState.IsValid)
            {
                (bool succeeded, Category category, string error) = await _repo.Add(model);
                if (succeeded) return Ok(category);
                return BadRequest(new { Message = error });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }


        [HttpPut]
        public async ValueTask<IActionResult> Put([FromBody] Category model)
        {
            if (ModelState.IsValid)
            {
                (bool succeeded, Category category, string error) = await _repo.Update(model);
                if (succeeded) return Ok(category);
                return BadRequest(new { Message = error });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }

        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> Delete(int id)
        {
            Category category = new Category { Id = id };
            string message;
            try
            {
                (bool succeeded, string error) = await _repo.Delete(category);
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
