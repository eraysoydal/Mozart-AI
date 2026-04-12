using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface ITrackStatisticRepositoryAsync : IGenericRepositoryAsync<TrackStatistic>
    {
        Task<int> GetTotalStreamsByTrackAsync(Guid trackId);
        Task<int> GetTotalStreamsByArtistAsync(string artistId);
        Task<int> GetMonthlyListenersByArtistAsync(string artistId);
        Task<int> GetTrackCountByArtistAsync(string artistId);
        Task<IReadOnlyList<TrackStatistic>> GetStreamHistoryAsync(string artistId, DateTime from, DateTime to);
    }
}
