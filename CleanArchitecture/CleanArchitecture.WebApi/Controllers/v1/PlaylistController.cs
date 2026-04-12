using CleanArchitecture.Core.Features.Playlists.Commands.AddTrackToPlaylist;
using CleanArchitecture.Core.Features.Playlists.Commands.CreatePlaylist;
using CleanArchitecture.Core.Features.Playlists.Commands.DeletePlaylist;
using CleanArchitecture.Core.Features.Playlists.Commands.RemoveTrackFromPlaylist;
using CleanArchitecture.Core.Features.Playlists.Commands.UpdatePlaylist;
using CleanArchitecture.Core.Features.Playlists.Queries.GetPlaylistById;
using CleanArchitecture.Core.Features.Playlists.Queries.GetPlaylistsByUser;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class PlaylistController : BaseApiController
    {
        private readonly IAuthenticatedUserService _authenticatedUser;

        public PlaylistController(IAuthenticatedUserService authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Create a new playlist.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> Create(CreatePlaylistCommand command)
        {
            command.UserId = _authenticatedUser.UserId;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Update playlist information.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> Update(Guid id, UpdatePlaylistCommand command)
        {
            if (id != command.Id) return BadRequest();
            command.UserId = _authenticatedUser.UserId;
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete a playlist.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeletePlaylistCommand { Id = id, UserId = _authenticatedUser.UserId }));
        }

        /// <summary>
        /// Add a track to a playlist.
        /// </summary>
        [HttpPost("{playlistId}/tracks/{trackId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> AddTrack(Guid playlistId, Guid trackId)
        {
            return Ok(await Mediator.Send(new AddTrackToPlaylistCommand { PlaylistId = playlistId, TrackId = trackId, UserId = _authenticatedUser.UserId }));
        }

        /// <summary>
        /// Remove a track from a playlist.
        /// </summary>
        [HttpDelete("{playlistId}/tracks/{trackId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> RemoveTrack(Guid playlistId, Guid trackId)
        {
            return Ok(await Mediator.Send(new RemoveTrackFromPlaylistCommand { PlaylistId = playlistId, TrackId = trackId, UserId = _authenticatedUser.UserId }));
        }

        /// <summary>
        /// Get a playlist with its tracks.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPlaylistByIdViewModel))]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await Mediator.Send(new GetPlaylistByIdQuery { Id = id, CurrentUserId = _authenticatedUser.UserId }));
        }

        /// <summary>
        /// Get paginated playlists for a user.
        /// </summary>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<GetPlaylistsByUserViewModel>))]
        public async Task<IActionResult> GetByUser(string userId, [FromQuery] GetPlaylistsByUserParameter filter)
        {
            return Ok(await Mediator.Send(new GetPlaylistsByUserQuery
            {
                UserId = userId,
                CurrentUserId = _authenticatedUser.UserId,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            }));
        }
    }
}
