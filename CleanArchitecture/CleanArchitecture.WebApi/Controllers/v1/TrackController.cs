using System;
using System.Threading.Tasks;
using CleanArchitecture.Core.Features.Tracks.Commands.CreateTrack;
using CleanArchitecture.Core.Features.Tracks.Commands.DeleteTrack;
using CleanArchitecture.Core.Features.Tracks.Commands.UpdateTrack;
using CleanArchitecture.Core.Features.Tracks.Queries.GetAllTracks;
using CleanArchitecture.Core.Features.Tracks.Queries.GetTrackById;
using CleanArchitecture.Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Get a music track by id. Requires authentication.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetTrackByIdViewModel))]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetTrackByIdQuery { Id = id }));
        }

        /// <summary>
        /// Update a music track.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(Guid id, UpdateTrackCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete a music track.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteTrackCommand { Id = id }));
        }
    }
}
