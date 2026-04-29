using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;

using System;
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

        public async Task<IReadOnlyList<Track>> GetPagedReponseAsync(
            int pageNumber,
            int pageSize,
            string searchQuery = null,
            int? genreId = null,
            string artistName = null,
            bool? isAiGenerated = null)
        {
            var query = _tracks.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
                query = query.Where(t => t.Title.Contains(searchQuery));

            if (genreId.HasValue)
                query = query.Where(t => t.GenreId == genreId.Value);

            if (!string.IsNullOrEmpty(artistName))
                query = query.Where(t => t.Artist.Username.Contains(artistName));

            if (isAiGenerated.HasValue)
                query = query.Where(t => t.IsAiGenerated == isAiGenerated.Value);

            return await query
                .Include(t => t.Genre)
                .Include(t => t.Artist)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Track> GetByIdAsync(Guid id)
        {
            return await _tracks
                .Include(t => t.Genre)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
