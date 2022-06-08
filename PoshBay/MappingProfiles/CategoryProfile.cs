using PoshBay.Data;
using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;

namespace PoshBay.Profile
{
    public class CategoryProfile : AutoMapper.Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryViewModel, Category>()
                            .ForMember(d => d.CreatededBy, opt => opt.MapFrom(x => x.UserName))
                            .ReverseMap();
        }
    }
}
