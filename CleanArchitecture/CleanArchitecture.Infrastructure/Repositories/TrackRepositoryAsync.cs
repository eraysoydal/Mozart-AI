using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class TrackRepositoryAsync : GenericRepositoryAsync<Track>, ITrackRepositoryAsync
    {
        private readonly DbSet<Track> _tracks;

        public TrackRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _tracks = dbContext.Set<Track>();
        }

        public new async Task<IReadOnlyList<Track>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _tracks
                .Include(t => t.Genre)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
