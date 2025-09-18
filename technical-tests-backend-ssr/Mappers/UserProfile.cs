using AutoMapper;
using Kanban.Domain.Models;
using technical_tests_backend_ssr.Dto.Request;
using technical_tests_backend_ssr.Dto.Response;

namespace technical_tests_backend_ssr.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());

            CreateMap<User, UserResponseDto>();
        }
    }
}
