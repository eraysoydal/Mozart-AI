using System;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface ITrackRepositoryAsync : IGenericRepositoryAsync<Track>
    {
        Task<Track> GetByIdAsync(Guid id);
    }
}
