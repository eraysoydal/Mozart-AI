using CleanArchitecture.Core.Features.Comments.Commands.CreateComment;
using CleanArchitecture.Core.Features.Comments.Commands.DeleteComment;
using CleanArchitecture.Core.Features.Comments.Queries.GetCommentsByTrack;
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
    public class CommentController : BaseApiController
    {
        private readonly IAuthenticatedUserService _authenticatedUser;

        public CommentController(IAuthenticatedUserService authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Post a new comment or reply to a track.
        /// </summary>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> Post(CreateCommentCommand command)
        {
            command.UserId = _authenticatedUser.UserId; // Assign from context
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Delete a comment authored by the current user.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteCommentCommand
            {
                Id = id,
                UserId = _authenticatedUser.UserId
            };
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Get paginated comments for a specific track.
        /// </summary>
        [HttpGet("track/{trackId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<GetCommentsByTrackViewModel>))]
        public async Task<IActionResult> GetByTrack(Guid trackId, [FromQuery] GetCommentsByTrackParameter filter)
        {
            return Ok(await Mediator.Send(new GetCommentsByTrackQuery
            {
                TrackId = trackId,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            }));
        }
    }
}
