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
    public class CategoriesController : GenericController<Category>
    {
        public CategoriesController(IModelManager<Category> repo): base(repo)
        {}

        [HttpGet]
        public override async ValueTask<IActionResult> Get()
        {
            ICollection<Category> categories = await _repo
                                                .Item()
                                                .Include(c => c.Images)
                                                    .ThenInclude(i => i.Tags)
                                                .ToListAsync();
            return Ok(categories);

        }

        [HttpGet("{id:int}")]
        public override async ValueTask<IActionResult> Get(int id)
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
    }
}
