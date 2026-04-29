using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IArtistApplicationRepositoryAsync : IGenericRepositoryAsync<ArtistApplication>
    {
        Task<ArtistApplication> GetByIdGuidAsync(Guid id);
        Task<IReadOnlyList<ArtistApplication>> GetPendingApplicationsAsync();
        Task<ArtistApplication> GetByUserIdAsync(string userId);
        Task<bool> HasPendingApplicationAsync(string userId);
    }
}
