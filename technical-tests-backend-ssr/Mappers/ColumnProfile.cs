using AutoMapper;
using Kanban.Domain.Models;
using Kanban.Contracts.Dto.Request;
using Kanban.Contracts.Dto.Response;

namespace technical_tests_backend_ssr.Mappers
{
    public class ColumnProfile : Profile
    {
        public ColumnProfile()
        {
            // Si tienes ColumnRequest, agrega el mapeo aquí
            // CreateMap<ColumnRequest, Column>();
            CreateMap<Column, ColumnResponse>()
                .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks))
                .ForMember(dest => dest.Board, opt => opt.MapFrom(src => src.Board));
        }
    }
}
