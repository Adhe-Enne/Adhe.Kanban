using AutoMapper;
using Kanban.Business.Services.Interfaces;
using Kanban.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using technical_tests_backend_ssr.Dto.Request;
using technical_tests_backend_ssr.Dto.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace technical_tests_backend_ssr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService; // Servicio de generación de JWT

        public UserController(IUserService userService, IMapper mapper, IJwtService jwtService)
        {
            _userService = userService;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRequestDto dto)
        {
            var userEntity = _mapper.Map<User>(dto);
            var user = await _userService.RegisterAsync(userEntity, dto.Password);
            var response = _mapper.Map<UserResponseDto>(user);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRequestDto dto)
        {
            User? user = await _userService.AuthenticateAsync(dto.Email, dto.Password);

            if (user == null) return Unauthorized();

            var token = _jwtService.GenerateToken(user);
            var response = _mapper.Map<UserResponseDto>(user);
            return Ok(new { user = response, token });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            var response = _mapper.Map<IEnumerable<UserResponseDto>>(users);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(new { UserId = userId });
        }
    }
}
