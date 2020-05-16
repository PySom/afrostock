﻿using Microsoft.AspNetCore.Mvc;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AfrroStock.Models.ViewModels;
using AfrroStock.Models.DTOs;
using System.Collections.Generic;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class CollectionsController : GenericController<Collection, CollectionVM, CollectionDTO>
    {
        public CollectionsController(IModelManager<Collection> repo, IMapper mapper) : base(repo, mapper)
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
            return Ok(_mapper.Map<ICollection<Collection>, ICollection<CollectionDTO>>(model));
        }

        [HttpPost]
        [Authorize(Roles = "Both,Collector,Super")]
        public override ValueTask<IActionResult> Post([FromBody] CollectionVM model)
        {
            return base.Post(model);
        }
    }
}
