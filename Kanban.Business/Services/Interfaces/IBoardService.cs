using Kanban.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace Kanban.Business.Services.Interfaces
{
    public interface IBoardService: IService<Board>
    {
        Task<IEnumerable<Board>> GetByOwnerAsync(Guid ownerId);
    }
}
