using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Infrastructure.Contexts;
using CleanArchitecture.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class ArtistApplicationRepositoryAsync : GenericRepositoryAsync<ArtistApplication>, IArtistApplicationRepositoryAsync
    {
        private readonly ApplicationDbContext _dbContext;

        public ArtistApplicationRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ArtistApplication> GetByIdGuidAsync(Guid id)
        {
            return await _dbContext.ArtistApplications
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IReadOnlyList<ArtistApplication>> GetPendingApplicationsAsync()
        {
            return await _dbContext.ArtistApplications
                .Include(a => a.User)
                .Where(a => a.StatusId == ApplicationStatus.Pending)
                .OrderByDescending(a => a.Id)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ArtistApplication> GetByUserIdAsync(string userId)
        {
            return await _dbContext.ArtistApplications
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.UserId == userId);
        }

        public async Task<bool> HasPendingApplicationAsync(string userId)
        {
            return await _dbContext.ArtistApplications
                .AnyAsync(a => a.UserId == userId && a.StatusId == ApplicationStatus.Pending);
        }
    }
}
