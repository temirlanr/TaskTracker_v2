using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker_v2.Dtos;
using TaskTracker_v2.Entities;
using TaskTracker_v2.Exceptions;
using TaskTracker_v2.Extensions;
using TaskTracker_v2.Services;

namespace TaskTracker_v2.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        public ProjectController(IProjectService service, IMapper mapper)
        {
            _projectService = service;
            _mapper = mapper;
        }

        // GET: api/projects
        /// <summary>
        /// Returns information about all Projects
        /// </summary>
        /// <returns>A list of Projects</returns>
        /// <response code="200">Request processed</response>
        /// <response code="400">Error occured while processing, check the Exception info please</response> 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<ProjectReadDto>> GetProjects()
        {
            try
            {
                var projects = _projectService.GetProjects();
                return Ok(_mapper.Map<IEnumerable<ProjectReadDto>>(projects));
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/projects/{projectId}
        /// <summary>
        /// Returns information about Project given its ID
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>Project info</returns>
        /// <response code="200">Request processed</response>
        /// <response code="400">Error occured while processing, check the Exception info please</response>
        [HttpGet("{projectId}", Name = "GetProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ProjectReadDto> GetProject(int projectId)
        {
            try
            {
                var project = _projectService.GetProjectById(projectId);
                return Ok(_mapper.Map<ProjectReadDto>(project));
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST: api/projects
        /// <summary>
        /// Creates a Project
        /// </summary>
        /// <remarks>
        /// Project status fields accepts only: "NotStarted", "Active", "Completed". 
        /// Task status field accepts only: "ToDo", "InProgress", "Done".
        /// </remarks>
        /// <param name="projectCreateDto"></param>
        /// <returns>A newly created Project</returns>
        /// <response code="201">Returns the newly created Project</response>
        /// <response code="400">Error occured while processing, check the Exception info please</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ProjectCreateDto> CreateProject(ProjectCreateDto projectCreateDto)
        {
            try
            {
                var project = _mapper.Map<Project>(projectCreateDto);
                _projectService.CreateProject(project);
                var projectReadDto = _mapper.Map<ProjectReadDto>(project);
                return CreatedAtAction(nameof(GetProject), new { projectId = projectReadDto.Id }, projectReadDto);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT: api/projects/{projectId}
        /// <summary>
        /// Updates Project info
        /// </summary>
        /// <remarks>
        /// Project status fields accepts only: "NotStarted", "Active", "Completed".
        /// </remarks>
        /// <param name="projectId"></param>
        /// <param name="projectUpdateDto"></param>
        /// <returns>No content</returns>
        /// <response code="204">Request successfully processed</response>
        /// <response code="400">Error occured while processing, check the Exception info please</response>
        [HttpPut("{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateProject(int projectId, ProjectUpdateDto projectUpdateDto)
        {
            try
            {
                var existingProject = _projectService.GetProjectById(projectId);
                _mapper.Map(projectUpdateDto, existingProject);
                _projectService.UpdateProject(existingProject);
                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/projects/{projectId}
        /// <summary>
        /// Deletes an instance of Project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>No content</returns>
        /// <response code="204">Request successfully processed</response>
        /// <response code="400">Error occured while processing, check the Exception info please</response>
        [HttpDelete("{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteProject(int projectId)
        {
            try
            {
                _projectService.DeleteProject(projectId);
                return NoContent();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        // PATCH: api/projects/{projectId}
        /// <summary>
        /// Updates Project info 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PATCH api/projects/{projectId}
        ///     [
        ///         {
        ///             "op": "replace",
        ///             "path": "name",
        ///             "value": "SomeName"
        ///         }
        ///     ]
        ///     
        /// There can be made several operations (body is a list of operations). 
        /// Project status fields accepts only: "NotStarted", "Active", "Completed".
        /// More info here: https://docs.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-6.0
        /// </remarks>
        /// <param name="projectId"></param>
        /// <param name="patchDoc"></param>
        /// <returns>No content</returns>
        /// <response code="204">Request successfully processed</response>
        /// <response code="400">Error occured while processing, check the Exception info please</response>
        [HttpPatch("{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateProjectPartial(int projectId, JsonPatchDocument<ProjectUpdateDto> patchDoc)
        {
            try
            {
                var existingProject = _projectService.GetProjectById(projectId);
                var projectToPatch = _mapper.Map<ProjectUpdateDto>(existingProject);

                patchDoc.ApplyTo(projectToPatch, ModelState);

                if (!TryValidateModel(projectToPatch))
                {
                    return BadRequest(ModelState);
                }

                _mapper.Map(projectToPatch, existingProject);
                _projectService.UpdateProject(existingProject);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
