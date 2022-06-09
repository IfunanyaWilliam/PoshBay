using PoshBay.Data;
using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;

namespace PoshBay.Profile
{
    public class CategoryProfile : AutoMapper.Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryViewModel, Category>().ReverseMap();
            CreateMap<CategoryEditViewModel, Category>();

            CreateMap<Category, CategoryEditViewModel>();
        }
    }
}
