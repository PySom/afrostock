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
    public class CollectionsController : GenericController<Collection>
    {
        public CollectionsController(IModelManager<Collection> repo): base(repo)
        {}

        [HttpGet]
        public override async ValueTask<IActionResult> Get()
        {
            ICollection<Collection> options = await _repo
                                                .Item()
                                                .Include(c => c.Categories)
                                                    .ThenInclude(i => i.Images)
                                                .ToListAsync();
            return Ok(options);

        }

        [HttpGet("{id:int}")]
        public override async ValueTask<IActionResult> Get(int id)
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
    }
}
