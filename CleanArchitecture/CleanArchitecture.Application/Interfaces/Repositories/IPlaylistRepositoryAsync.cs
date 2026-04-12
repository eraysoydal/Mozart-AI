using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IPlaylistRepositoryAsync : IGenericRepositoryAsync<Playlist>
    {
        Task<Playlist> GetByIdAsync(Guid id);
        Task<Playlist> GetByIdWithTracksAsync(Guid id);
        Task<IReadOnlyList<Playlist>> GetByUserAsync(string userId, int pageNumber, int pageSize);
        Task AddTrackToPlaylistAsync(Guid playlistId, Guid trackId);
        Task RemoveTrackFromPlaylistAsync(Guid playlistId, Guid trackId);
        Task<bool> HasTrackAsync(Guid playlistId, Guid trackId);
    }
}
