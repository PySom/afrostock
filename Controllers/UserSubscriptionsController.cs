using Microsoft.AspNetCore.Mvc;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class UserCSubscriptionsController : GenericController<UserSubscription>
    {
        public UserCSubscriptionsController(IModelManager<UserSubscription> repo): base(repo)
        {}
    }
}
