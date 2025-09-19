using Kanban.Business.Services.Interfaces;
using Kanban.Domain.Models;
using Kanban.Repository.Interfaces;

namespace Kanban.Business.Services
{
    public class BoardService(IRepository<Board> repository) : Service<Board>(repository), IBoardService
    {
        public async Task<IEnumerable<Board>> GetByOwnerAsync(Guid ownerId)
        {
            return await _repository.FindAsync(b => b.OwnerId == ownerId && b.IsActive);
        }
    }
}
