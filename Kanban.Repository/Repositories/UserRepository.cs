using Kanban.DatabaseContext;
using Kanban.Domain.Models;
using Kanban.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Repository.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TechnicalTestDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.AsNoTracking()
                               .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
