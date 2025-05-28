using AutoMapper;
using Ecomm.Application.DTOs;
using Ecomm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm.Tests.TestMappings
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>();
        }
    }
}
