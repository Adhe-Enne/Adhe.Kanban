using AutoMapper;
using Kanban.Domain.Models;
using Kanban.Contracts.Dto.Request;
using Kanban.Contracts.Dto.Response;

namespace technical_tests_backend_ssr.Mappers
{
    public class BoardProfile : Profile
    {
        public BoardProfile()
        {
            CreateMap<BoardRequestDto, Board>();
            CreateMap<Board, BoardResponse>()
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.Columns, opt => opt.MapFrom(src => src.Columns))
                .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks));
        }
    }
}
