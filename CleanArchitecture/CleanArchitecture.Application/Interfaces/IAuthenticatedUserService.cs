namespace CleanArchitecture.Core.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        bool IsAdmin { get; }
    }
}
