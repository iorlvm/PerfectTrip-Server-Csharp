using AutoMapper;
using PerfectTrip.Application.DTOs.Member;
using PerfectTrip.Common.Entities.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Customer, UserDto>();
            CreateMap<Company, UserDto>();
            CreateMap<Admin, UserDto>();
        }
    }
}
