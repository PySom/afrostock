using Microsoft.AspNetCore.Mvc;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class SubscriptionsController : GenericController<Subscription>
    {
        public SubscriptionsController(IModelManager<Subscription> repo): base(repo)
        {}
    }
}
