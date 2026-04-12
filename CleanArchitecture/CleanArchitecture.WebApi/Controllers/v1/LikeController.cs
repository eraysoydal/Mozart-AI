using CleanArchitecture.Core.Features.Likes.Commands.ToggleLike;
using CleanArchitecture.Core.Features.Likes.Queries.CheckLikeStatus;
using CleanArchitecture.Core.Features.Likes.Queries.GetLikesByTrack;
using CleanArchitecture.Core.Features.Likes.Queries.GetLikesByUser;
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
    public class LikeController : BaseApiController
    {
        private readonly IAuthenticatedUserService _authenticatedUser;

        public LikeController(IAuthenticatedUserService authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Toggles like status (Like if not liked, Unlike if liked).
        /// </summary>
        [HttpPost("{trackId}/toggle")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> ToggleLike(Guid trackId)
        {
            var command = new ToggleLikeCommand
            {
                TrackId = trackId,
                UserId = _authenticatedUser.UserId
            };
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Check if the current user has liked the track.
        /// </summary>
        [HttpGet("{trackId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> CheckStatus(Guid trackId)
        {
            var query = new CheckLikeStatusQuery
            {
                TrackId = trackId,
                UserId = _authenticatedUser.UserId
            };
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Gets all users who liked a track (paginated).
        /// </summary>
        [HttpGet("track/{trackId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<GetLikesByTrackViewModel>))]
        public async Task<IActionResult> GetByTrack(Guid trackId, [FromQuery] GetLikesByTrackParameter filter)
        {
            return Ok(await Mediator.Send(new GetLikesByTrackQuery
            {
                TrackId = trackId,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            }));
        }

        /// <summary>
        /// Gets all tracks liked by a specific user (paginated).
        /// </summary>
        [HttpGet("user/{userId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<GetLikesByUserViewModel>))]
        public async Task<IActionResult> GetByUser(string userId, [FromQuery] GetLikesByUserParameter filter)
        {
            return Ok(await Mediator.Send(new GetLikesByUserQuery
            {
                UserId = userId,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            }));
        }
    }
}
