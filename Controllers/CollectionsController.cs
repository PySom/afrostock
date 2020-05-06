using Microsoft.AspNetCore.Mvc;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

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
            var model = await _repo.Item()
                                    .Include(co => co.Collectibles)
                                        .ThenInclude(cl => cl.Image)
                                            .ThenInclude(i => i.Author)
                                    .Include(co => co.Collectibles)
                                        .ThenInclude(cl => cl.Collector)
                                    .ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        [Authorize(Roles = "Both,Collector,Super")]
        public override ValueTask<IActionResult> Post([FromBody] Collection model)
        {
            return base.Post(model);
        }
    }
}
