namespace Kanban.Business.Services.Interfaces
{
    public interface IStateService<T>
    {
        Task ActivateEntity(Guid id);
        Task InactivateEntity(Guid id);
    }
}
