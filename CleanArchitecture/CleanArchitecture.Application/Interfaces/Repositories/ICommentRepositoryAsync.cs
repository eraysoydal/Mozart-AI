using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface ICommentRepositoryAsync : IGenericRepositoryAsync<Comment>
    {
        Task<Comment> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Comment>> GetByTrackAsync(Guid trackId, int pageNumber, int pageSize);
        Task<int> GetCountByTrackAsync(Guid trackId);
    }
}
