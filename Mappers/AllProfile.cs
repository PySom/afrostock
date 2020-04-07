using AutoMapper;
using AfrroStock.Models;
using AfrroStock.Models.ViewModels;

namespace AfrroStock.Mappers
{
    public class AllProfile : Profile
    {
        public AllProfile()
        {
            CreateMap<ApplicationUser, RegisterViewModel>()
                .ReverseMap();
            CreateMap<ApplicationUser, UserViewModel>()
                .ReverseMap();
        }
    }
}
