using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IFollowerRepositoryAsync
    {
        Task<Follower> GetAsync(string followerId, string artistId);
        Task<IReadOnlyList<Follower>> GetFollowersByArtistAsync(string artistId, int pageNumber, int pageSize);
        Task<IReadOnlyList<Follower>> GetFollowingByUserAsync(string userId, int pageNumber, int pageSize);
        Task<int> GetFollowerCountAsync(string artistId);
        Task<int> GetFollowingCountAsync(string userId);
        Task<Follower> AddAsync(Follower entity);
        Task DeleteAsync(Follower entity);
        Task<bool> ExistsAsync(string followerId, string artistId);
    }
}
