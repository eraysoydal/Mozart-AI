using System;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IShareRepositoryAsync
    {
        Task<Share> AddAsync(Share entity);
        Task<int> GetCountByTrackAsync(Guid trackId);
        Task<bool> ExistsAsync(string userId, Guid trackId);
    }
}
