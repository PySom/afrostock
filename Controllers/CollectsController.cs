using Microsoft.AspNetCore.Mvc;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class CollectsController : GenericController<Collect>
    {
        public CollectsController(IModelManager<Collect> repo): base(repo)
        {}

        [HttpPost]
        [Authorize(Roles = "Both,Collector,Super")]
        public override ValueTask<IActionResult> Post([FromBody] Collect model)
        {
            return base.Post(model);
        }
    }
}
