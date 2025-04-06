using AutoMapper;
using Identity.API.Dtos.Request;
using Identity.API.Dtos.Response;
using Identity.API.Models;

namespace Identity.API.Mappers.Profiles
{
    public class ApplicationUserMappingsProfile : Profile
    {
        public ApplicationUserMappingsProfile()
        {
            CreateMap<CreateApplicationUserRequestDto, ApplicationUserModel>();

            CreateMap<UpdateApplicationUserRequestDto, ApplicationUserModel>();

            CreateMap<ApplicationUserModel, UserResponseDto>();
        }
    }
}
