using AutoMapper;
using Kanban.Domain.Models;
using Kanban.Contracts.Dto.Request;
using Kanban.Contracts.Dto.Response;
using Task = Kanban.Domain.Models.Task;
namespace technical_tests_backend_ssr.Mappers
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskRequest, Task>();
            CreateMap<Task, TaskResponse>();
        }
    }
}
