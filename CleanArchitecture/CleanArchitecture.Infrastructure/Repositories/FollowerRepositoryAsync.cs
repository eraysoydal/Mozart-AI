using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class FollowerRepositoryAsync : IFollowerRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public FollowerRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Follower> GetAsync(string followerId, string artistId)
        {
            return await _dbContext.Set<Follower>()
                .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.ArtistId == artistId);
        }

        public async Task<IReadOnlyList<Follower>> GetFollowersByArtistAsync(string artistId, int pageNumber, int pageSize)
        {
            return await _dbContext.Set<Follower>()
                .Include(f => f.FollowerUser)
                .Where(f => f.ArtistId == artistId)
                .OrderByDescending(f => f.FollowedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Follower>> GetFollowingByUserAsync(string userId, int pageNumber, int pageSize)
        {
            return await _dbContext.Set<Follower>()
                .Include(f => f.Artist)
                .Where(f => f.FollowerId == userId)
                .OrderByDescending(f => f.FollowedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetFollowerCountAsync(string artistId)
        {
            return await _dbContext.Set<Follower>()
                .CountAsync(f => f.ArtistId == artistId);
        }

        public async Task<int> GetFollowingCountAsync(string userId)
        {
            return await _dbContext.Set<Follower>()
                .CountAsync(f => f.FollowerId == userId);
        }

        public async Task<Follower> AddAsync(Follower entity)
        {
            await _dbContext.Set<Follower>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Follower entity)
        {
            _dbContext.Set<Follower>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string followerId, string artistId)
        {
            return await _dbContext.Set<Follower>()
                .AnyAsync(f => f.FollowerId == followerId && f.ArtistId == artistId);
        }
    }
}
