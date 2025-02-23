using AutoMapper;
using Identity.Domain.Entities;
using Identity.Infrastructure.Models;

namespace Identity.Infrastructure.Mappers.Profiles
{
    public class ApplicationRoleMappingsProfile : Profile
    {
        public ApplicationRoleMappingsProfile()
        {
            CreateMap<ApplicationRoleModel, ApplicationRole>()
                .ReverseMap();
        }
    }
}
