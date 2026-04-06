using AutoMapper;
using CleanArchitecture.Core.Features.Users.Queries.GetAllUsers;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Users.Queries.GetAllArtists
{
    public class GetAllArtistsQuery : IRequest<PagedResponse<GetAllUsersViewModel>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllArtistsQueryHandler : IRequestHandler<GetAllArtistsQuery, PagedResponse<GetAllUsersViewModel>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IMapper _mapper;

        public GetAllArtistsQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<GetAllUsersViewModel>> Handle(GetAllArtistsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllArtistsParameter>(request);
            var users = await _userRepository.GetPagedArtistsResponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var userViewModels = _mapper.Map<List<GetAllUsersViewModel>>(users);
            return new PagedResponse<GetAllUsersViewModel>(userViewModels, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
