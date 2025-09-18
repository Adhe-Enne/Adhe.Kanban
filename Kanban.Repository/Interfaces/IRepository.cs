using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Obtener todos
        Task<IEnumerable<TEntity>> GetAllAsync();

        // Obtener por Id
        Task<TEntity?> GetByIdAsync(Guid id);

        // Filtrar con expresión (lambda)
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        // Crear
        Task<TEntity> AddAsync(TEntity entity);

        // Actualizar
        Task UpdateAsync(TEntity entity);

        // Eliminar
        Task DeleteAsync(TEntity entity);

        // Verificar existencia
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
