using AutoMapper;
using SudelMerck.DiscountService.Model.Entities;
using SudelMerck.DiscountService.Model.Services;

namespace SudelMerck.DiscountService.Infrastructure.MappingProfile
{
    public class DiscountMappingProfile:Profile
    {
        public DiscountMappingProfile()
        {
            CreateMap<DiscountCode, DiscountDto>().ReverseMap();
        }
    }
}
