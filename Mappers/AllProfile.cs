using AutoMapper;
using AfrroStock.Models;
using AfrroStock.Models.ViewModels;
using AfrroStock.Models.DTOs;

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
            CreateMap<ApplicationUser, UserDTO>(MemberList.Source)
                .ReverseMap();
            //CreateMap<Category, CategoryVM>()
            //    .ReverseMap();
            //CreateMap<Category, CategoryDTO>()
            //    .ReverseMap();
            CreateMap<Collect, CollectVM>()
                .ReverseMap();
            CreateMap<Collect, CollectDTO>()
                .ReverseMap();
            CreateMap<Collection, CollectionVM>()
                .ReverseMap();
            CreateMap<Collection, CollectionDTO>()
                .ReverseMap();
            CreateMap<CollectionType, CollectionTypeVM>()
                .ReverseMap();
            CreateMap<CollectionType, CollectionTypeDTO>()
              .ReverseMap();
            CreateMap<Image, ImageVM>()
                .ReverseMap();
            CreateMap<Image, ImageDTO>()
                .ReverseMap();
            CreateMap<Subscription, SubscriptionVM>()
                .ReverseMap();
            CreateMap<Subscription, SubscriptionDTO>()
                .ReverseMap();
            CreateMap<Tag, TagVM>()
                .ReverseMap();
            CreateMap<Tag, TagDTO>()
                .ReverseMap();
            CreateMap<UserImage, UserImageVM>()
                .ReverseMap();
            CreateMap<UserImage, UserImageDTO>()
                .ReverseMap();
            CreateMap<UserSubscription, UserSubscriptionVM>()
                .ReverseMap();
            CreateMap<UserSubscription, UserSubscriptionDTO>()
                .ReverseMap();
            CreateMap<ImageTag, ImageTagVM>()
                .ReverseMap();
            CreateMap<ImageTag, ImageTagDTO>()
                .ReverseMap();
        }
    }
}
