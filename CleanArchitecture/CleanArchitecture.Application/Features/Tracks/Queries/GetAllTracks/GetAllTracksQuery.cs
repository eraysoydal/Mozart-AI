using AutoMapper;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Tracks.Queries.GetAllTracks
{
    public class GetAllTracksQuery : IRequest<PagedResponse<GetAllTracksViewModel>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchQuery { get; set; }
    }

    public class GetAllTracksQueryHandler : IRequestHandler<GetAllTracksQuery, PagedResponse<GetAllTracksViewModel>>
    {
        private readonly ITrackRepositoryAsync _trackRepository;
        private readonly IMapper _mapper;
        private readonly ICloudFrontService _cloudFrontService;

        public GetAllTracksQueryHandler(
            ITrackRepositoryAsync trackRepository,
            IMapper mapper,
            ICloudFrontService cloudFrontService)
        {
            _trackRepository = trackRepository;
            _mapper = mapper;
            _cloudFrontService = cloudFrontService;
        }

        public async Task<PagedResponse<GetAllTracksViewModel>> Handle(GetAllTracksQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllTracksParameter>(request);
            var tracks = await _trackRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize, validFilter.SearchQuery);
            var trackViewModels = _mapper.Map<List<GetAllTracksViewModel>>(tracks);

            // Replace FileUrl with signed CloudFront URLs
            foreach (var vm in trackViewModels)
            {
                if (!string.IsNullOrEmpty(vm.FileUrl))
                    vm.FileUrl = _cloudFrontService.GetSignedUrl(vm.FileUrl);
            }

            return new PagedResponse<GetAllTracksViewModel>(trackViewModels, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}

