using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Dtos;
using TaskTracker_v2.Entities;

namespace TaskTracker_v2.Profiles
{
    public class ProjectProfiles : Profile
    {
        public ProjectProfiles()
        {
            CreateMap<ProjectUpdateDto, Project>();
            CreateMap<Project, ProjectUpdateDto>();
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<Project, ProjectReadDto>();
        }
    }
}
