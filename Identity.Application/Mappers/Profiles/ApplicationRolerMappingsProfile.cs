using AutoMapper;
using Identity.Application.Dtos.Request;
using Identity.Application.Dtos.Response;
using Identity.Domain.Entities;

namespace Identity.Application.Mappers.Profiles
{
    public class ApplicationRoleMappingsProfile : Profile
    {
        public ApplicationRoleMappingsProfile()
        {
            CreateMap<CreateApplicationRoleRequestDto, ApplicationRole>();

            CreateMap<UpdateApplicationRoleRequestDto, ApplicationRole>();

            CreateMap<ApplicationRole, RoleResponseDto>();
        }
    }
}
