using System;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class ShareRepositoryAsync : IShareRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public ShareRepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Share> AddAsync(Share entity)
        {
            await _dbContext.Set<Share>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<int> GetCountByTrackAsync(Guid trackId)
        {
            return await _dbContext.Set<Share>()
                .CountAsync(s => s.TrackId == trackId);
        }

        public async Task<bool> ExistsAsync(string userId, Guid trackId)
        {
            return await _dbContext.Set<Share>()
                .AnyAsync(s => s.UserId == userId && s.TrackId == trackId);
        }
    }
}
