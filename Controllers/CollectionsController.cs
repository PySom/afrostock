using Microsoft.AspNetCore.Mvc;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class CollectionsController : GenericController<Collection>
    {
        public CollectionsController(IModelManager<Collection> repo): base(repo)
        {}

        [HttpPost]
        [Authorize(Roles = "Both,Collector,Super")]
        public override ValueTask<IActionResult> Post([FromBody] Collection model)
        {
            return base.Post(model);
        }
    }
}
