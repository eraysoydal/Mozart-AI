using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IUserRepositoryAsync : IGenericRepositoryAsync<User>
    {
        Task<User> GetByIdStringAsync(string id);
        Task<System.Collections.Generic.IReadOnlyList<User>> GetPagedArtistsResponseAsync(int pageNumber, int pageSize);
    }
}
