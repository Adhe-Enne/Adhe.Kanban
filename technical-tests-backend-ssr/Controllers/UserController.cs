using AutoMapper;
using Kanban.Business.Exceptions;
using Kanban.Business.Services.Interfaces;
using Kanban.Contracts.Constants;
using Kanban.Contracts.Dto.Request;
using Kanban.Contracts.Dto.Response;
using Kanban.Contracts.Results;
using Kanban.Contracts.Results.Interfaces;
using Kanban.Domain.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace technical_tests_backend_ssr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class UserController(IUserService userService, ILogger<UserController> logger, IMapper mapper) : BaseController(logger, mapper)
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<ActionResult<ApiResult<UserResponse>>> GetAll()
        {
            IApiResult<List<UserResponse>> result = new ApiResult<List<UserResponse>>();

            _log.LogInformation(Messages.USER_GETALL);

            try
            {
                var users = await _userService.GetAllAsync();
                result.Data = _mapper.Map<List<UserResponse>>(users);
                if (result.Data.Any())
                {
                    result.Set(HandleSuccess(string.Format(Messages.USER_GETALL_OK, result.Data.Count), HttpStatusCode.OK));
                }
                else
                {
                    result.Set(HandleSuccess(Messages.USER_NOFOUND_OK, HttpStatusCode.NoContent));
                }

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

        [HttpPut("/role/{id}")]
        public async Task<ActionResult<IApiResult>> UpdateRole(Guid id, [FromBody] UpdateRoleRequest dto)
        {
            IApiResult result = new ApiResult();

            _log.LogInformation(Messages.USER_UPDATEROLE, id, dto.Role);

            try
            {
                await _userService.UpdateRoleAsync(id, dto.Role);
                result.Set(HandleSuccess(Messages.USER_UPDATEROLE_OK, HttpStatusCode.NoContent));
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

        [HttpPut("/activate-user/{id}")]
        public async Task<ActionResult<IApiResult>> Activate(Guid id)
        {
            var result = new ApiResult();

            _log.LogInformation(Messages.USER_ACTIVATING, id);

            try
            {
                await _userService.ActivateUserAsync(id);
                result.Set(HandleSuccess(Messages.USER_ACTIVATE_OK, HttpStatusCode.NoContent));
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


        [HttpDelete("/inactivate/{id}")]
        public async Task<ActionResult<IApiResult>> InactiveUser(Guid id)
        {
            var result = new ApiResult();

            _log.LogInformation(Messages.USER_DELETING, id);

            try
            {
                await _userService.InactivateUserAsync(id);
                result.Set(HandleSuccess(Messages.USER_DELETING_OK, HttpStatusCode.NoContent));
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
