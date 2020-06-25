using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApplication3.Dtos;
using WebApplication3.Models;

namespace WebApplication3.AppStart
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<ApplicationUserDto, ApplicationUser>();

            CreateMap<ScheduleDto, Schedule>();
            CreateMap<Schedule, ScheduleDto>();

            CreateMap<TypesOfServicesDto, TypesOfServices>();
            CreateMap<TypesOfServices, TypesOfServicesDto>();

            CreateMap<UserAddress, UserAddressDto>();
            CreateMap<UserAddressDto, UserAddress>();


        }
    }
}
