using AutoMapper;
using LoginServiceDapr.Model;
using LoginServiceDapr.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginServiceDapr.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Person, User>();
        }
    }
}
