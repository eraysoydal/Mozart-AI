using CleanArchitecture.Core.Features.Tracks.Queries.GetAllTracks;
using CleanArchitecture.Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;

using CleanArchitecture.Core.Features.Tracks.Commands.CreateTrack;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class TrackController : BaseApiController
    {
        /// <summary>
        /// Get all music tracks (paginated). Requires authentication.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<GetAllTracksViewModel>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<PagedResponse<GetAllTracksViewModel>> Get([FromQuery] GetAllTracksParameter filter)
        {
            return await Mediator.Send(new GetAllTracksQuery
            {
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            });
        }

        /// <summary>
        /// Create a new music track.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> Post(CreateTrackCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
