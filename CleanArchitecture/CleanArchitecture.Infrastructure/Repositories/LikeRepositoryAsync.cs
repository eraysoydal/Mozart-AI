using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class LikeRepositoryAsync : ILikeRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public LikeRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Like> GetAsync(string userId, Guid trackId)
        {
            return await _dbContext.Set<Like>()
                .FirstOrDefaultAsync(l => l.UserId == userId && l.TrackId == trackId);
        }

        public async Task<IReadOnlyList<Like>> GetByUserAsync(string userId, int pageNumber, int pageSize)
        {
            return await _dbContext.Set<Like>()
                .Include(l => l.Track)
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.LikedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Like>> GetByTrackAsync(Guid trackId, int pageNumber, int pageSize)
        {
            return await _dbContext.Set<Like>()
                .Include(l => l.User)
                .Where(l => l.TrackId == trackId)
                .OrderByDescending(l => l.LikedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetCountByTrackAsync(Guid trackId)
        {
            return await _dbContext.Set<Like>()
                .CountAsync(l => l.TrackId == trackId);
        }

        public async Task<Like> AddAsync(Like entity)
        {
            await _dbContext.Set<Like>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Like entity)
        {
            _dbContext.Set<Like>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string userId, Guid trackId)
        {
            return await _dbContext.Set<Like>()
                .AnyAsync(l => l.UserId == userId && l.TrackId == trackId);
        }
    }
}
