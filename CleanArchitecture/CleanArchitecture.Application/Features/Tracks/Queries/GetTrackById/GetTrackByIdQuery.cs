using AutoMapper;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Tracks.Queries.GetTrackById
{
    public class GetTrackByIdQuery : IRequest<GetTrackByIdViewModel>
    {
        public Guid Id { get; set; }

        public class GetTrackByIdQueryHandler : IRequestHandler<GetTrackByIdQuery, GetTrackByIdViewModel>
        {
            private readonly ITrackRepositoryAsync _trackRepository;
            private readonly IMapper _mapper;
            private readonly ICloudFrontService _cloudFrontService;

            public GetTrackByIdQueryHandler(ITrackRepositoryAsync trackRepository, IMapper mapper, ICloudFrontService cloudFrontService)
            {
                _trackRepository = trackRepository;
                _mapper = mapper;
                _cloudFrontService = cloudFrontService;
            }

            public async Task<GetTrackByIdViewModel> Handle(GetTrackByIdQuery query, CancellationToken cancellationToken)
            {
                var track = await _trackRepository.GetByIdAsync(query.Id);
                if (track == null) throw new EntityNotFoundException("Track", query.Id);

                var viewModel = _mapper.Map<GetTrackByIdViewModel>(track);
                if (!string.IsNullOrEmpty(viewModel.FileUrl))
                    viewModel.FileUrl = _cloudFrontService.GetSignedUrl(viewModel.FileUrl);
                if (!string.IsNullOrEmpty(viewModel.CanvasUrl))
                    viewModel.CanvasUrl = _cloudFrontService.GetSignedUrl(viewModel.CanvasUrl);
                if (!string.IsNullOrEmpty(viewModel.CoverImageUrl))
                    viewModel.CoverImageUrl = _cloudFrontService.GetSignedUrl(viewModel.CoverImageUrl);
                
                return viewModel;
            }
        }
    }
}
