using AutoMapper;
using Hdn.Rewards.Domain.DTO;
using Hdn.Rewards.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hdn.Rewards.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<User, UserDto>()
                .ReverseMap();
        }
    }
}
