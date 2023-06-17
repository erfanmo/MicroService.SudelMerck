using AutoMapper;
using SudelMerck.BasketService.Model.Entities;
using SudelMerck.BasketService.Model.Services;

namespace SudelMerck.BasketService.Infrastracture.MappingProfile
{
    public class BasketMappingProfile :Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<BasketItem, AddItemBasektDto>().ReverseMap();
        }
    }
}
