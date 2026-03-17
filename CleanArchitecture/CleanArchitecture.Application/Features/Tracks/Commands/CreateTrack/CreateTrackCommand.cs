using AutoMapper;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Tracks.Commands.CreateTrack
{
    public class CreateTrackCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string ArtistId { get; set; }
        public string FileUrl { get; set; }
        public int? GenreId { get; set; }
        public bool IsAiGenerated { get; set; }
        public DateTime ReleaseDate { get; set; }
        public byte AudioFormatId { get; set; }
        public int AiPermission { get; set; }
        public bool AllowSystemAnalysis { get; set; }
    }

    public class CreateTrackCommandHandler : IRequestHandler<CreateTrackCommand, Guid>
    {
        private readonly ITrackRepositoryAsync _trackRepository;
        private readonly IMapper _mapper;

        public CreateTrackCommandHandler(ITrackRepositoryAsync trackRepository, IMapper mapper)
        {
            _trackRepository = trackRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTrackCommand request, CancellationToken cancellationToken)
        {
            var track = _mapper.Map<Track>(request);
            await _trackRepository.AddAsync(track);
            return track.Id;
        }
    }
}
