using AutoMapper;
using Kanban.Business.Exceptions;
using Kanban.Business.Services.Interfaces;
using Kanban.Contracts.Constants;
using Kanban.Contracts.Dto.Request;
using Kanban.Contracts.Dto.Response;
using Kanban.Contracts.Results;
using Kanban.Contracts.Results.Interfaces;
using Kanban.Domain.Models;
using Kanban.Domain.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using TaskEntity = Kanban.Domain.Models.Task;

namespace technical_tests_backend_ssr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController(ITaskService taskService, ILogger<TaskController> logger, IMapper mapper) : BaseController(logger, mapper)
    {
        private readonly ITaskService _taskService = taskService;

        [HttpGet("board/{boardId}")]
        public async Task<ActionResult<IApiResult<List<TaskResponse>>>> GetTasksByBoard(Guid boardId)
        {
            IApiResult<List<TaskResponse>> result = new ApiResult<List<TaskResponse>>();

            _log.LogInformation(Messages.TASK_BY_BOARD, boardId);

            try
            {
                var tasks = await _taskService.GetByBoardAsync(boardId);
                result.Data = _mapper.Map<List<TaskResponse>>(tasks);

                if (result.Data.Count == 0)
                    result.Set(HandleSuccess(HttpStatusCode.NoContent, Messages.TASK_BY_BOARD_NOFOUND_OK, boardId));
                else
                    result.Set(HandleSuccess(Messages.TASK_BY_BOARD_OK, boardId));
            }
            catch (BusinessException ex)
            {
                result.Set(HandleServiceException(ex));
            }
            catch (Exception ex)
            {
                result.Set(HandleException(ex, HttpStatusCode.InternalServerError, Messages.ERROR));
            }

            return ResponseApi(result);
        }

        [HttpGet("column/{columnId}")]
        public async Task<ActionResult<IApiResult<List<TaskResponse>>>> GetTasksByColumn(Guid columnId)
        {
            IApiResult<List<TaskResponse>> result = new ApiResult<List<TaskResponse>>();
            _log.LogInformation(Messages.TASK_BY_COLUMN, columnId);

            try
            {
                var tasks = await _taskService.GetByColumnAsync(columnId);
                result.Data = _mapper.Map<List<TaskResponse>>(tasks);

                if (result.Data.Count == 0)
                    result.Set(HandleSuccess(HttpStatusCode.NoContent, Messages.TASK_BY_COLUMN_NOFOUNDL_OK, columnId));
                else
                    result.Set(HandleSuccess(Messages.TASK_BY_COLUMN_OK, columnId));
            }
            catch (BusinessException ex)
            {
                result.Set(HandleServiceException(ex));
            }
            catch (Exception ex)
            {
                result.Set(HandleException(ex, HttpStatusCode.InternalServerError, Messages.ERROR));
            }
            return ResponseApi(result);
        }

        [HttpGet("assigned/me")]
        public async Task<ActionResult<IApiResult<List<TaskResponse>>>> GetMyTasks()
        {
            IApiResult<List<TaskResponse>> result = new ApiResult<List<TaskResponse>>();

            try
            {
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                _log.LogInformation(Messages.TASK_BY_USER, userId);

                var tasks = await _taskService.GetByAssignedUserAsync(userId);
                result.Data = _mapper.Map<List<TaskResponse>>(tasks);

                if (result.Data.Count == 0)
                    result.Set(HandleSuccess(HttpStatusCode.NoContent, Messages.TASK_BY_USER_NOFOUNDL_OK, userId));
                else
                    result.Set(HandleSuccess(Messages.TASK_BY_USER_OK, userId));
            }
            catch (BusinessException ex)
            {
                result.Set(HandleServiceException(ex));
            }
            catch (Exception ex)
            {
                result.Set(HandleException(ex, HttpStatusCode.InternalServerError, Messages.ERROR));
            }

            return ResponseApi(result);
        }

        [HttpPost]
        public async Task<ActionResult<IApiResult<TaskResponse>>> CreateTask([FromBody] TaskRequest dto)
        {
            IApiResult<TaskResponse> result = new ApiResult<TaskResponse>();
            _log.LogInformation(Messages.TASK_CREATE, dto.Title);

            try
            {
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

                TaskEntity task = _mapper.Map<TaskEntity>(dto);
                task.AssignedUserId = userId;
                TaskEntity createdTask = await _taskService.AddAsync(task);

                result.Data = _mapper.Map<TaskResponse>(createdTask);
                result.Set(HandleSuccess(Messages.TASK_CREATE_OK, createdTask.Id));
            }
            catch (BusinessException ex)
            {
                result.Set(HandleServiceException(ex));
            }
            catch (Exception ex)
            {
                result.Set(HandleException(ex, HttpStatusCode.InternalServerError, Messages.ERROR));
            }

            return ResponseApi(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IApiResult<TaskResponse>>> UpdateTask(Guid id, [FromBody] TaskRequest dto)
        {
            IApiResult<TaskResponse> result = new ApiResult<TaskResponse>();
            _log.LogInformation(Messages.TASK_UPDATE, id);

            try
            {
                TaskEntity task = _mapper.Map<TaskEntity>(dto);
                task.Id = id;
                await _taskService.UpdateAsync(task);

                result.Data = _mapper.Map<TaskResponse>(task);
                result.Set(HandleSuccess(HttpStatusCode.NoContent, Messages.TASK_UPDATE_OK, task.Id));
            }
            catch (BusinessException ex)
            {
                result.Set(HandleServiceException(ex));
            }
            catch (Exception ex)
            {
                result.Set(HandleException(ex, HttpStatusCode.InternalServerError, Messages.ERROR));
            }
            return ResponseApi(result);
        }

        [HttpDelete("/inactivate-user/{id}")]
        public async Task<ActionResult<IApiResult<TaskResponse>>> Delete(Guid id)
        {
            IApiResult<TaskResponse> result = new ApiResult<TaskResponse>();
            _log.LogInformation(Messages.TASK_DELETE, id);

            try
            {
                TaskEntity? task = await _taskService.GetByIdAsync(id);
                if (task is null)
                    throw new BusinessException($"Task Id {id} not found", Kanban.Business.Enums.BusinessErrorCode.EntityNotFound);

                await _taskService.InactivateEntity(task.Id);

                result.Set(HandleSuccess(HttpStatusCode.NoContent, Messages.TASK_DELETE_OK, task.Id));
            }
            catch (BusinessException ex)
            {
                result.Set(HandleServiceException(ex));
            }
            catch (Exception ex)
            {
                result.Set(HandleException(ex, HttpStatusCode.InternalServerError, Messages.ERROR));
            }

            return ResponseApi(result);
        }

        [HttpDelete("/activate/{id}")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<ActionResult<IApiResult<TaskResponse>>> UndoDelete(Guid id)
        {
            IApiResult<TaskResponse> result = new ApiResult<TaskResponse>();
            _log.LogInformation(Messages.TASK_ACTIVATE, id);

            try
            {
                TaskEntity? task = await _taskService.GetByIdAsync(id);
                if (task is null)
                    throw new BusinessException($"Task Id {id} not found", Kanban.Business.Enums.BusinessErrorCode.EntityNotFound);

                await _taskService.ActivateEntity(task.Id);

                result.Set(HandleSuccess(HttpStatusCode.NoContent, Messages.TASK_ACTIVATE_OK, task.Id));
            }
            catch (BusinessException ex)
            {
                result.Set(HandleServiceException(ex));
            }
            catch (Exception ex)
            {
                result.Set(HandleException(ex, HttpStatusCode.InternalServerError, Messages.ERROR));
            }

            return ResponseApi(result);
        }

        [HttpGet("/inactives")]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<ActionResult<IApiResult<List<TaskResponse>>>> GetAllInactives()
        {
            IApiResult<List<TaskResponse>> result = new ApiResult<List<TaskResponse>>();
            _log.LogInformation(Messages.TASK_GETALL_INACTIVE);

            try
            {
                var tasks = await _taskService.FindAsync(x=> x.IsActive == false);
                result.Data = _mapper.Map<List<TaskResponse>>(tasks);

                result.Set(HandleSuccess(HttpStatusCode.NoContent, Messages.TASK_ACTIVATE_OK, result.Data.Count));
            }
            catch (BusinessException ex)
            {
                result.Set(HandleServiceException(ex));
            }
            catch (Exception ex)
            {
                result.Set(HandleException(ex, HttpStatusCode.InternalServerError, Messages.ERROR));
            }

            return ResponseApi(result);
        }
    }
}