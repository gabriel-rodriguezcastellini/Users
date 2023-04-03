using ApplicationCore.Entities.UserAggregate;
using AutoMapper;
using Users.Api.Models;

namespace Users.Api.Mapping;

public class EntityToDto : Profile
{
    public EntityToDto()
    {
        CreateMap<User, UserDto>();
    }
}
