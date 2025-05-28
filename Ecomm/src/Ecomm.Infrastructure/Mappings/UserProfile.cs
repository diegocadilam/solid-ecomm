using AutoMapper;
using Ecomm.Application.DTOs;
using Ecomm.Domain.Entities;

namespace Ecomm.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
