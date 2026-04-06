using CleanArchitecture.Core.Enums;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string BackgroundPhotoUrl { get; set; }
        public UserRole RoleId { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, string>
    {
        private readonly IUserRepositoryAsync _userRepository;

        public UpdateUserCommandHandler(IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdStringAsync(command.Id);

            if (user == null)
            {
                throw new ApiException($"User Not Found.");
            }
            else
            {
                user.Username = command.Username;
                user.Email = command.Email;
                user.ProfilePhotoUrl = command.ProfilePhotoUrl;
                user.BackgroundPhotoUrl = command.BackgroundPhotoUrl;
                user.RoleId = command.RoleId;

                await _userRepository.UpdateAsync(user);
                return user.Id;
            }
        }
    }
}
