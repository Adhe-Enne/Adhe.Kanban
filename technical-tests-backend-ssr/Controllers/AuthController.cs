using AutoMapper;
using Kanban.Business.Exceptions;
using Kanban.Business.Services.Interfaces;
using Kanban.Contracts.Constants;
using Kanban.Contracts.Dto.Request;
using Kanban.Contracts.Dto.Response;
using Kanban.Contracts.Results;
using Kanban.Contracts.Results.Interfaces;
using Kanban.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace technical_tests_backend_ssr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService, IJwtService jwtService, ILogger<BoardController> logger, IMapper mapper) : BaseController(logger, mapper)
    {
        private readonly IUserService _userService = userService;
        private readonly IJwtService _jwtService = jwtService;

        [HttpPost(EndPoints.REGISTER)]
        public async Task<ActionResult<IApiResult<UserResponse>>> Register([FromBody]UserRegisterRequest dto)
        {
            IApiResult<UserResponse> result = new ApiResult<UserResponse>();

            _log.LogInformation(Messages.ERROR_USER_REG, new { email = dto.Email });

            try
            {
                var userEntity = _mapper.Map<User>(dto);
                var user = await _userService.RegisterAsync(userEntity, dto.Password);

                if(user is null)
                    return ResponseApi(new ApiResult(Messages.ERROR_CREATE_USER, HttpStatusCode.BadRequest));

                result.Data = _mapper.Map<UserResponse>(user);

                result.Set(HandleSuccess(Messages.USER_REGISTRING_OK,  dto.Email));
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

        [HttpPost(EndPoints.LOGIN)]
        public async Task<ActionResult<ILoginResult<LoginResponse>>> Login([FromBody]LoginRequest dto)
        {
            ILoginResult<LoginResponse> result = new LoginResult<LoginResponse>();

            _log.LogInformation(Messages.USER_LOGIN, dto.Email);

            try
            {
                User? user = await _userService.AuthenticateAsync(dto.Email, dto.Password);

                if (user == null)
                    return ResponseApi(new ApiResult(Messages.ERROR_INVALID_USER, HttpStatusCode.NotFound));

                result.Token = _jwtService.GenerateToken(user);
                result.UserData = _mapper.Map<LoginResponse>(user);
                result.Set(HandleSuccess(Messages.USER_LOGIN_OK));
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
