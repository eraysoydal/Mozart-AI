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
    public class TrackStatisticRepositoryAsync : GenericRepositoryAsync<TrackStatistic>, ITrackStatisticRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public TrackStatisticRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetTotalStreamsByTrackAsync(Guid trackId)
        {
            return await _dbContext.Set<TrackStatistic>()
                .Where(ts => ts.TrackId == trackId)
                .SumAsync(ts => ts.StreamCount);
        }

        public async Task<int> GetTotalStreamsByArtistAsync(string artistId)
        {
            return await _dbContext.Set<TrackStatistic>()
                .Include(ts => ts.Track)
                .Where(ts => ts.Track.ArtistId == artistId)
                .SumAsync(ts => ts.StreamCount);
        }

        public async Task<int> GetMonthlyListenersByArtistAsync(string artistId)
        {
            var thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);
            
            return await _dbContext.Set<TrackStatistic>()
                .Include(ts => ts.Track)
                .Where(ts => ts.Track.ArtistId == artistId && ts.Timestamp >= thirtyDaysAgo)
                .Select(ts => ts.ListenerId)
                .Distinct()
                .CountAsync();
        }

        public async Task<int> GetTrackCountByArtistAsync(string artistId)
        {
            return await _dbContext.Set<Track>()
                .CountAsync(t => t.ArtistId == artistId);
        }

        public async Task<IReadOnlyList<TrackStatistic>> GetStreamHistoryAsync(string artistId, DateTime from, DateTime to)
        {
            return await _dbContext.Set<TrackStatistic>()
                .Include(ts => ts.Track)
                .Where(ts => ts.Track.ArtistId == artistId && ts.Timestamp >= from && ts.Timestamp <= to)
                .OrderBy(ts => ts.Timestamp)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
