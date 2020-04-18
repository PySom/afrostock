using Microsoft.AspNetCore.Mvc;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class UserImagesController : GenericController<UserImage>
    {
        public UserImagesController(IModelManager<UserImage> repo): base(repo)
        {}
    }
}
