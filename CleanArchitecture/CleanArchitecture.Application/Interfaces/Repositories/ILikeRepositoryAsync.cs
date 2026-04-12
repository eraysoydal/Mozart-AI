using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface ILikeRepositoryAsync
    {
        Task<Like> GetAsync(string userId, Guid trackId);
        Task<IReadOnlyList<Like>> GetByUserAsync(string userId, int pageNumber, int pageSize);
        Task<IReadOnlyList<Like>> GetByTrackAsync(Guid trackId, int pageNumber, int pageSize);
        Task<int> GetCountByTrackAsync(Guid trackId);
        Task<Like> AddAsync(Like entity);
        Task DeleteAsync(Like entity);
        Task<bool> ExistsAsync(string userId, Guid trackId);
    }
}
