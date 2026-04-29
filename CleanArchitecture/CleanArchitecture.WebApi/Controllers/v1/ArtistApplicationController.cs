using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Core.Features.ArtistApplications.Commands.ApproveArtistApplication;
using CleanArchitecture.Core.Features.ArtistApplications.Commands.CreateArtistApplication;
using CleanArchitecture.Core.Features.ArtistApplications.Queries.GetPendingApplications;
using CleanArchitecture.Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ArtistApplicationController : CleanArchitecture.WebApi.Controllers.BaseApiController
    {
        /// <summary>
        /// Submit a new artist verification application. (Authenticated listeners only)
        /// </summary>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Guid>))]
        public async Task<IActionResult> Create([FromBody] CreateArtistApplicationCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Get all pending artist applications. (Admin only)
        /// </summary>
        [HttpGet("pending")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PendingApplicationDto>))]
        public async Task<IActionResult> GetPending()
        {
            return Ok(await Mediator.Send(new GetPendingApplicationsQuery()));
        }

        /// <summary>
        /// Approve or reject an artist application. (Admin only)
        /// </summary>
        [HttpPut("{id}/review")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<string>))]
        public async Task<IActionResult> Review(Guid id, [FromBody] ApproveArtistApplicationCommand command)
        {
            command.ApplicationId = id;
            return Ok(await Mediator.Send(command));
        }
    }
}
