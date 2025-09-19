using Kanban.Business.Enums;
using Kanban.Business.Exceptions;
using Kanban.Business.Services.Interfaces;
using Kanban.Domain.Models;
using Kanban.Repository.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace Kanban.Business.Services
{
    public class StateService<T>(IRepository<T> repository) : Service<T>(repository), IStateService<T> where T : BaseModel
    {
        public async Task InactivateEntity(Guid id)
        {
            await SetState(id, false);
        }

        public async Task ActivateEntity(Guid id)
        {
            await SetState(id, true);
        }

        private async Task SetState(Guid id, bool isActive)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                throw new BusinessException("Registro no encontrado", BusinessErrorCode.EntityNotFound);

            user.IsActive = isActive;
            await _repository.UpdateAsync(user);
        }
    }
}
