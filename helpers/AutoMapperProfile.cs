using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetAngularAuthWebApi.Models;
using NetAngularAuthWebApi.Models.Dto;

namespace NetAngularAuthWebApi.helpers
{
    public class AutoMapperProfile : Profile
    {
       public void AuthMapperProfile(){
         CreateMap<SchoolClass, SchoolClassDTO>();
       }
    }
}