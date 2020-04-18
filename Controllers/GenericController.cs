using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class GenericController<T> : ControllerBase where T : class, IModel, new()
    {
        protected readonly IModelManager<T> _repo;
        public GenericController(IModelManager<T> repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public virtual async ValueTask<IActionResult> Get()
        {
            ICollection<T> options = await _repo
                                            .Item()
                                            .ToListAsync();
            return Ok(options);

        }


        [HttpGet("{id:int}")]
        public virtual async ValueTask<IActionResult> Get(int id)
        {
            T model = await _repo
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
        public virtual async ValueTask<IActionResult> Post([FromBody] T model)
        {
            if (ModelState.IsValid)
            {
                (bool succeeded, T t, string error) = await _repo.Add(model);
                if (succeeded) return Ok(t);
                return BadRequest(new { Message = error });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }


        [HttpPut]
        public virtual async ValueTask<IActionResult> Put([FromBody] T model)
        {
            if (ModelState.IsValid)
            {
                (bool succeeded, T t, string error) = await _repo.Update(model);
                if (succeeded) return Ok(t);
                return BadRequest(new { Message = error });
            }
            return BadRequest(new { Errors = ModelState.Values.SelectMany(e => e.Errors).ToList() });
        }

        [HttpDelete("{id}")]
        public virtual async ValueTask<IActionResult> Delete(int id)
        {
            T t = new T { Id = id };
            string message;
            try
            {
                (bool succeeded, string error) = await _repo.Delete(t);
                message = error;
                if (succeeded) return NoContent();
                return NotFound();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                message = ex.Message + " " + ex.InnerException.Message;
            }
            return BadRequest(new { Message = message });
        }
    }
}
