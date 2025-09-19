using TaskEntity = Kanban.Domain.Models.Task;

namespace Kanban.Business.Services.Interfaces
{
    public interface ITaskService : IStateService<TaskEntity>, IService<TaskEntity>
    {
        Task<IEnumerable<TaskEntity>> GetByBoardAsync(Guid boardId);
        Task<IEnumerable<TaskEntity>> GetByColumnAsync(Guid columnId);
        Task<IEnumerable<TaskEntity>> GetByAssignedUserAsync(Guid userId);
    }
}
