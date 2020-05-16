using Microsoft.AspNetCore.Mvc;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AfrroStock.Models.ViewModels;
using AfrroStock.Models.DTOs;
using AutoMapper;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class CollectsController : GenericController<Collect, CollectVM, CollectDTO>
    {
        public CollectsController(IModelManager<Collect> repo, IMapper mapper): base(repo, mapper)
        {}

        [HttpPost]
        [Authorize(Roles = "Both,Collector,Super")]
        public override ValueTask<IActionResult> Post([FromBody] CollectVM model)
        {
            return base.Post(model);
        }
    }
}
