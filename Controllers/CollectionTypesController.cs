using Microsoft.AspNetCore.Mvc;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class CollectionTypesController : GenericController<CollectionType>
    {
        public CollectionTypesController(IModelManager<CollectionType> repo): base(repo)
        {}

        [HttpGet]
        public override async ValueTask<IActionResult> Get()
        {
            var model = await _repo.Item()
                                        .Include(c => c.Collections)
                                            .ThenInclude(co => co.Collectibles)
                                                .ThenInclude(cl => cl.Image)
                                        .Include(c => c.Collections)
                                            .ThenInclude(co => co.Collectibles)
                                                .ThenInclude(cl => cl.Collector)
                                         .ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        [Authorize(Roles = "Both,Collector,Super")]
        public override ValueTask<IActionResult> Post([FromBody] CollectionType model)
        {
            return base.Post(model);
        }
    }
}
