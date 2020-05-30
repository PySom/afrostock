using Microsoft.AspNetCore.Mvc;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AfrroStock.Models.ViewModels;
using AfrroStock.Models.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class CollectionsController : GenericController<Collection, CollectionVM, CollectionDTO>
    {
        public CollectionsController(IModelManager<Collection> repo, IMapper mapper) : base(repo, mapper)
        {}

        [HttpGet]
        public override async ValueTask<IActionResult> GetAll(int page = 1)
        {
            int itemsPerPage = 20;
            var model = await _repo.Item()
                                    .Include(co => co.Collectibles)
                                        .ThenInclude(cl => cl.Image)
                                            .ThenInclude(i => i.Author)
                                    .Include(co => co.Collectibles)
                                        .ThenInclude(cl => cl.Collector)
                                     .Skip(page * itemsPerPage - itemsPerPage)
                                     .Take(itemsPerPage)
                                    .ToListAsync();
            return Ok(_mapper.Map<ICollection<Collection>, ICollection<CollectionDTO>>(model));
        }

        [HttpGet("{id}")]
        public override async ValueTask<IActionResult> Get(int id)
        {
            var model = await _repo.Item()
                .Where(c => c.Id == id)
                .Include(co => co.Collectibles)
                    .ThenInclude(cl => cl.Image)
                        .ThenInclude(i => i.Author)
                .Include(co => co.Collectibles)

                    .ThenInclude(cl => cl.Collector)
                .FirstOrDefaultAsync();
            if (model != null)
            {
                return Ok(model);
            }
            return NotFound();
        }

        [HttpGet("byname/{name}")]
        public async ValueTask<IActionResult> GetName(string name)
        {
            var model = await _repo.Item()
                .Where(c => c.Name.ToLower() == name.ToLower())
                .Include(co => co.Collectibles)
                    .ThenInclude(cl => cl.Image)
                        .ThenInclude(i => i.Author)
                .Include(co => co.Collectibles)

                    .ThenInclude(cl => cl.Collector)
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

        [HttpPost]
        [Authorize(Roles = "Both,Collector,Super")]
        public override ValueTask<IActionResult> Post([FromBody] CollectionVM model)
        {
            return base.Post(model);
        }
    }
}
