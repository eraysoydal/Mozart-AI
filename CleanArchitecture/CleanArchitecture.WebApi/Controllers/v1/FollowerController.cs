using CleanArchitecture.Core.Features.Followers.Commands.ToggleFollow;
using CleanArchitecture.Core.Features.Followers.Queries.CheckFollowStatus;
using CleanArchitecture.Core.Features.Followers.Queries.GetFollowerCount;
using CleanArchitecture.Core.Features.Followers.Queries.GetFollowersByArtist;
using CleanArchitecture.Core.Features.Followers.Queries.GetFollowingByUser;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class FollowerController : BaseApiController
    {
        private readonly IAuthenticatedUserService _authenticatedUser;

        public FollowerController(IAuthenticatedUserService authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Toggles follow status for an artist.
        /// </summary>
        [HttpPost("{artistId}/toggle")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> ToggleFollow(string artistId)
        {
            var command = new ToggleFollowCommand
            {
                ArtistId = artistId,
                FollowerId = _authenticatedUser.UserId
            };
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Check if the current user is following the artist.
        /// </summary>
        [HttpGet("{artistId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> CheckStatus(string artistId)
        {
            var query = new CheckFollowStatusQuery
            {
                ArtistId = artistId,
                FollowerId = _authenticatedUser.UserId
            };
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Gets the total number of followers for an artist.
        /// </summary>
        [HttpGet("{artistId}/count")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetCount(string artistId)
        {
            return Ok(await Mediator.Send(new GetFollowerCountQuery { ArtistId = artistId }));
        }

        /// <summary>
        /// Gets the paginated list of followers of an artist.
        /// </summary>
        [HttpGet("artist/{artistId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<GetFollowersByArtistViewModel>))]
        public async Task<IActionResult> GetFollowersByArtist(string artistId, [FromQuery] GetFollowersByArtistParameter filter)
        {
            return Ok(await Mediator.Send(new GetFollowersByArtistQuery
            {
                ArtistId = artistId,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            }));
        }

        /// <summary>
        /// Gets the paginated list of artists the user is following.
        /// </summary>
        [HttpGet("following/{userId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<GetFollowingByUserViewModel>))]
        public async Task<IActionResult> GetFollowingByUser(string userId, [FromQuery] GetFollowingByUserParameter filter)
        {
            return Ok(await Mediator.Send(new GetFollowingByUserQuery
            {
                UserId = userId,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            }));
        }
    }
}
