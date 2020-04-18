﻿using Microsoft.AspNetCore.Mvc;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class TagsController : GenericController<Tag>
    {
        public TagsController(IModelManager<Tag> repo): base(repo)
        {}
    }
}
