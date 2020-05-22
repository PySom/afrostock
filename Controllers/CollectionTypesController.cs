using Microsoft.AspNetCore.Mvc;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AfrroStock.Models.ViewModels;
using AfrroStock.Models.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class CollectionTypesController : GenericController<CollectionType, CollectionTypeVM, CollectionTypeDTO>
    {
        public CollectionTypesController(IModelManager<CollectionType> repo, IMapper mapper): base(repo, mapper)
        {}

        [HttpGet]
        public override async ValueTask<IActionResult> GetAll(int page = 1)
        {
            int itemsPerPage = 20;
            var model = await _repo.Item()
                                        .Include(c => c.Collections)
                                            .ThenInclude(co => co.Collectibles)
                                                .ThenInclude(cl => cl.Image)
                                        .Include(c => c.Collections)
                                            .ThenInclude(co => co.Collectibles)
                                                .ThenInclude(cl => cl.Collector)
                                        .Skip(page * itemsPerPage - itemsPerPage)
                                        .Take(itemsPerPage)
                                         .ToListAsync();

            return Ok(_mapper.Map<ICollection<CollectionType>, ICollection<CollectionTypeDTO>>(model));
        }

        [HttpPost]
        [Authorize(Roles = "Both,Collector,Super")]
        public override ValueTask<IActionResult> Post([FromBody] CollectionTypeVM model)
        {
            return base.Post(model);
        }
    }
}
