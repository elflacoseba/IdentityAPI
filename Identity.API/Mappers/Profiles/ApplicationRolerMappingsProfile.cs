using AutoMapper;
using Identity.API.Dtos.Request;
using Identity.API.Dtos.Response;
using Identity.API.Models;

namespace Identity.API.Mappers.Profiles
{
    public class ApplicationRoleMappingsProfile : Profile
    {
        public ApplicationRoleMappingsProfile()
        {
            CreateMap<CreateApplicationRoleRequestDto, ApplicationRoleModel>();

            CreateMap<UpdateApplicationRoleRequestDto, ApplicationRoleModel>();

            CreateMap<ApplicationRoleModel, RoleResponseDto>();
        }
    }
}
