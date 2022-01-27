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
using TaskTracker_v2.Services;

namespace TaskTracker_v2.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        // GET: api/tasks/{taskId}
        /// <summary>
        /// Returns Task info given Task Id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns>Task object</returns>
        /// <response code="200">Request processed</response>
        /// <response code="400">Error occured while processing, check the Exception info please</response>
        [HttpGet]
        [Route("{taskId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TaskReadDto> GetTask(int taskId)
        {
            try
            {
                var task = _taskService.GetTaskById(taskId);
                return Ok(_mapper.Map<TaskReadDto>(task));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/tasks
        /// <summary>
        /// Returns the list of all Tasks
        /// </summary>
        /// <returns>List of Tasks</returns>
        /// <response code="200">Request processed</response>
        /// <response code="400">Error occured while processing, check the Exception info please</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<TaskReadDto>> GetAllTasks()
        {
            try
            {
                var tasks = _taskService.GetTasks();
                return Ok(_mapper.Map<IEnumerable<TaskReadDto>>(tasks));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET: api/tasks/project{projectId}
        /// <summary>
        /// Returns the list of Tasks given a Project Id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>List of Tasks</returns>
        /// <response code="200">Request processed</response>
        /// <response code="400">Error occured while processing, check the Exception info please</response>
        [HttpGet]
        [Route("project{projectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<TaskReadDto>> GetTasksByProjectId(int projectId)
        {
            try
            {
                var tasks = _taskService.GetTasksByProjectId(projectId);
                return Ok(_mapper.Map<IEnumerable<TaskReadDto>>(tasks));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST: api/tasks
        /// <summary>
        /// Adds a Task to the Task
        /// </summary>
        /// <remarks>
        /// Task status field accepts only: "ToDo", "InProgress", "Done".
        /// </remarks>
        /// <param name="taskCreateDto"></param>
        /// <returns>Created Task</returns>
        /// <response code="201">Returns the newly created Task</response>
        /// <response code="400">Error occured while processing, check the Exception info please</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TaskCreateDto> CreateTask(TaskCreateDto taskCreateDto)
        {
            try
            {
                var task = _mapper.Map<ProjectTask>(taskCreateDto);
                _taskService.CreateTask(task);
                var taskReadDto = _mapper.Map<TaskReadDto>(task);
                return CreatedAtAction(nameof(GetTask), new { taskId = taskReadDto.Id }, taskReadDto);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE: api/tasks/{taskId}
        /// <summary>
        /// Deletes a Task given Task Id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns>No content</returns>
        /// <response code="204">Request successfully processed</response>
        /// <response code="400">Error occured while processing, check the Exception info please</response>
        [HttpDelete]
        [Route("{taskId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteTask(int taskId)
        {
            try
            {
                _taskService.DeleteTask(taskId);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT: api/tasks/{taskId}
        /// <summary>
        /// Updates Task info
        /// </summary>
        /// <remarks>
        /// Task status field accepts only: "ToDo", "InProgress", "Done".
        /// </remarks>
        /// <param name="taskId"></param>
        /// <param name="taskUpdateDto"></param>
        /// <returns>No content</returns>
        /// <response code="204">Request successfully processed</response>
        /// <response code="400">Error occured while processing, check the Exception info please</response>
        [HttpPut]
        [Route("{taskId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateTask(int taskId, TaskUpdateDto taskUpdateDto)
        {
            try
            {
                var existingTask = _taskService.GetTaskById(taskId);
                _mapper.Map(taskUpdateDto, existingTask);
                _taskService.UpdateTask(existingTask);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PATCH: api/tasks/{taskId}
        /// <summary>
        /// Updates Task info 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     
        ///     PATCH api/tasks/{taskId}
        ///     [
        ///         {
        ///             "op": "replace",
        ///             "path": "name",
        ///             "value": "SomeName"
        ///         }
        ///     ]
        ///     
        /// There can be made several operations (body is a list of operations). 
        /// Task status fields accepts only: "ToDo", "InProgress", "Done".
        /// More info here: https://docs.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-6.0
        /// </remarks>
        /// <param name="taskId"></param>
        /// <param name="patchDoc"></param>
        /// <returns>No content</returns>
        /// <response code="204">Request successfully processed</response>
        /// <response code="400">Error occured while processing, check the Exception info please</response>
        [HttpPatch]
        [Route("{taskId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateTaskPartial(int taskId, JsonPatchDocument<TaskUpdateDto> patchDoc)
        {
            try
            {
                var existingTask = _taskService.GetTaskById(taskId);
                var taskToPatch = _mapper.Map<TaskUpdateDto>(existingTask);

                patchDoc.ApplyTo(taskToPatch, ModelState);

                if (!TryValidateModel(taskToPatch))
                {
                    return BadRequest(ModelState);
                }

                _mapper.Map(taskToPatch, existingTask);
                _taskService.UpdateTask(existingTask);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
