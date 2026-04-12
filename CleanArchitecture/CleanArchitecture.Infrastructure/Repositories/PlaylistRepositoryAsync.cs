using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class PlaylistRepositoryAsync : GenericRepositoryAsync<Playlist>, IPlaylistRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public PlaylistRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Playlist> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<Playlist>().FindAsync(id);
        }

        public async Task<Playlist> GetByIdWithTracksAsync(Guid id)
        {
            return await _dbContext.Set<Playlist>()
                .Include(p => p.User)
                .Include(p => p.PlaylistTracks)
                    .ThenInclude(pt => pt.Track)
                        .ThenInclude(t => t.Artist)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Playlist>> GetByUserAsync(string userId, int pageNumber, int pageSize)
        {
            return await _dbContext.Set<Playlist>()
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddTrackToPlaylistAsync(Guid playlistId, Guid trackId)
        {
            var playlistTrack = new PlaylistTrack
            {
                PlaylistId = playlistId,
                TrackId = trackId
            };
            
            await _dbContext.Set<PlaylistTrack>().AddAsync(playlistTrack);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveTrackFromPlaylistAsync(Guid playlistId, Guid trackId)
        {
            var playlistTrack = await _dbContext.Set<PlaylistTrack>()
                .FirstOrDefaultAsync(pt => pt.PlaylistId == playlistId && pt.TrackId == trackId);
                
            if (playlistTrack != null)
            {
                _dbContext.Set<PlaylistTrack>().Remove(playlistTrack);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> HasTrackAsync(Guid playlistId, Guid trackId)
        {
            return await _dbContext.Set<PlaylistTrack>()
                .AnyAsync(pt => pt.PlaylistId == playlistId && pt.TrackId == trackId);
        }
    }
}
