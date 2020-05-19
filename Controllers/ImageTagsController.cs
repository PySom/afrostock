﻿using Microsoft.AspNetCore.Mvc;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;
using AfrroStock.Models.DTOs;
using AfrroStock.Models.ViewModels;
using AutoMapper;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class ImageTagsController : GenericController<ImageTag, ImageTagVM, ImageTagDTO>
    {
        public ImageTagsController(IModelManager<ImageTag> repo, IMapper mapper): base(repo, mapper)
        {}
    }
}