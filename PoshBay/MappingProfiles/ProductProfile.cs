using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;
using PoshBay.DTO;

namespace PoshBay.MappingProfiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, Product>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<Product, ProductDetailViewModel>();
            CreateMap<ProductDetailViewModel, Product>();
            CreateMap<ProductEditDTO, Product>();
            CreateMap<Product, ProductEditDTO>();
        }
    }
}
