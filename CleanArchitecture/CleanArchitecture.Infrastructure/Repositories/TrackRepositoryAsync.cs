using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class TrackRepositoryAsync : GenericRepositoryAsync<Track>, ITrackRepositoryAsync
    {
        public TrackRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
