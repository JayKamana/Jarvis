using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Jarvis.Dtos;
using Jarvis.Models;

namespace Jarvis.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<FRD, FRDDtos>();


            Mapper.CreateMap<FRDDtos, FRD>()
            .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}