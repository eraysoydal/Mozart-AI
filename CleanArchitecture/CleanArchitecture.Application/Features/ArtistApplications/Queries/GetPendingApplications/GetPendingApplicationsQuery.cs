using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Core.Features.ArtistApplications.Queries.GetPendingApplications
{
    public class PendingApplicationDto
    {
        public System.Guid Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PortfolioLinks { get; set; }
        public string IdProofUrl { get; set; }
    }

    public class GetPendingApplicationsQuery : IRequest<List<PendingApplicationDto>> { }

    public class GetPendingApplicationsQueryHandler : IRequestHandler<GetPendingApplicationsQuery, List<PendingApplicationDto>>
    {
        private readonly IArtistApplicationRepositoryAsync _repository;

        public GetPendingApplicationsQueryHandler(IArtistApplicationRepositoryAsync repository)
        {
            _repository = repository;
        }

        public async Task<List<PendingApplicationDto>> Handle(GetPendingApplicationsQuery request, CancellationToken cancellationToken)
        {
            var applications = await _repository.GetPendingApplicationsAsync();

            var result = new List<PendingApplicationDto>();
            foreach (var app in applications)
            {
                result.Add(new PendingApplicationDto
                {
                    Id = app.Id,
                    UserId = app.UserId,
                    UserName = app.User?.Username,
                    PortfolioLinks = app.PortfolioLinks,
                    IdProofUrl = app.IdProofUrl
                });
            }

            return result;
        }
    }
}
