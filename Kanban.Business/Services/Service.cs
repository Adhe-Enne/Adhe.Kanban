using Kanban.Business.Services.Interfaces;
using Kanban.Repository.Interfaces;
using System.Linq.Expressions;

namespace Kanban.Business.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;

        public Service(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.FindAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.ExistsAsync(predicate);
        }
    }
}
