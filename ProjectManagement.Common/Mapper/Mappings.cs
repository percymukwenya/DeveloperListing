using AutoMapper;
using ProjectManagement.Common.Models;
using ProjectManagement.Common.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Common.Mapper
{
    public class Mappings: Profile
    {
        public Mappings()
        {
            CreateMap<Developer, DeveloperDTO>().ReverseMap();
            CreateMap<Developer, AddDeveloperDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
        }
    }
}
