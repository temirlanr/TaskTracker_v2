using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Entities;
using TaskTracker_v2.Dtos;

namespace TaskTracker_v2.Profiles
{
    public class TaskProfiles : Profile
    {
        public TaskProfiles()
        {
            CreateMap<TaskCreateDto, ProjectTask>();
            CreateMap<ProjectTask, TaskReadDto>();
            CreateMap<ProjectTask, TaskUpdateDto>();
            CreateMap<TaskUpdateDto, ProjectTask>();
        }
    }
}
