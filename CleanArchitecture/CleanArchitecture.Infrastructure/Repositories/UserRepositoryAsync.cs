using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class UserRepositoryAsync : GenericRepositoryAsync<User>, IUserRepositoryAsync
    {
        private readonly DbSet<User> _users;

        public UserRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _users = dbContext.Set<User>();
        }

        public async Task<User> GetByIdStringAsync(string id)
        {
            return await _users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<System.Collections.Generic.IReadOnlyList<User>> GetPagedArtistsResponseAsync(int pageNumber, int pageSize)
        {
            return await _users
                .Where(u => u.RoleId == CleanArchitecture.Core.Enums.UserRole.Artist)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
