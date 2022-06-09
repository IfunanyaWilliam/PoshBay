using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;

namespace PoshBay.MappingProfiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>();
        }
    }
}
