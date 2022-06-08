using PoshBay.Data.Models;
using PoshBay.Data.ViewModels;

namespace PoshBay.MappingProfiles
{
    public class RegisterProfile : AutoMapper.Profile
    {
        public RegisterProfile()
        {
            CreateMap<RegisterViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, RegisterViewModel>();
        }
    }
}
