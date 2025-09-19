using Kanban.Business.Services.Interfaces;
using Kanban.Repository.Interfaces;
using TaskEntity = Kanban.Domain.Models.Task;

namespace Kanban.Business.Services
{
    public class TaskService(IRepository<TaskEntity> repository) : StateService<TaskEntity>(repository), ITaskService
    {
        public async Task<IEnumerable<TaskEntity>> GetByBoardAsync(Guid boardId)
        {
            return await _repository.FindAsync(t => t.BoardId == boardId && t.IsActive);
        }

        public async Task<IEnumerable<TaskEntity>> GetByColumnAsync(Guid columnId)
        {
            return await _repository.FindAsync(t => t.ColumnId == columnId && t.IsActive);
        }

        public async Task<IEnumerable<TaskEntity>> GetByAssignedUserAsync(Guid userId)
        {
            return await _repository.FindAsync(t => t.AssignedUserId == userId && t.IsActive);
        }
    }
}
