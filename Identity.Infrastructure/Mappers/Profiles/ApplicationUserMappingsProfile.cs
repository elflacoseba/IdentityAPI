using AutoMapper;
using Identity.Domain.Entities;
using Identity.Infrastructure.Models;

namespace Identity.Infrastructure.Mappers.Profiles
{
    public class ApplicationUserMappingsProfile : Profile
    {
        public ApplicationUserMappingsProfile()
        {
            CreateMap<ApplicationUserModel, ApplicationUser>()
                .ReverseMap();
        }
    }
}
