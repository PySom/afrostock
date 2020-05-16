using Microsoft.AspNetCore.Mvc;
using AfrroStock.Models;
using AfrroStock.Repository.Generic;
using AfrroStock.Models.ViewModels;
using AfrroStock.Models.DTOs;
using AutoMapper;

namespace AfrroStock.Controllers
{
    [Route("api/[controller]")]
    public class SubscriptionsController : GenericController<Subscription, SubscriptionVM, SubscriptionDTO>
    {
        public SubscriptionsController(IModelManager<Subscription> repo, IMapper mapper): base(repo, mapper)
        {}
    }
}
