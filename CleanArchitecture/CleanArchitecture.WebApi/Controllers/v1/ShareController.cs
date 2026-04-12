using CleanArchitecture.Core.Features.Shares.Commands.RecordShare;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class ShareController : BaseApiController
    {
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ShareController(IAuthenticatedUserService authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Record a track share to a platform.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> RecordShare(RecordShareCommand command)
        {
            command.UserId = _authenticatedUser.UserId;
            return Ok(await Mediator.Send(command));
        }
    }
}
