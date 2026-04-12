using CleanArchitecture.Core.Features.Analytics.Commands.RecordStream;
using CleanArchitecture.Core.Features.Analytics.Queries.GetDashboardStats;
using CleanArchitecture.Core.Features.Analytics.Queries.GetStreamHistory;
using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class AnalyticsController : BaseApiController
    {
        private readonly IAuthenticatedUserService _authenticatedUser;

        public AnalyticsController(IAuthenticatedUserService authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }

        /// <summary>
        /// Records a track stream by a user. (Listener ID can be null for anonymous streams).
        /// </summary>
        [HttpPost("stream")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<IActionResult> RecordStream(RecordStreamCommand command)
        {
            if (_authenticatedUser.UserId != null)
            {
                command.ListenerId = _authenticatedUser.UserId;
            }
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Gets dashboard statistic cards for an artist.
        /// </summary>
        [HttpGet("dashboard/{artistId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDashboardStatsViewModel))]
        public async Task<IActionResult> GetDashboardStats(string artistId)
        {
            return Ok(await Mediator.Send(new GetDashboardStatsQuery { ArtistId = artistId }));
        }

        /// <summary>
        /// Gets stream history for plotting charts for an artist.
        /// </summary>
        [HttpGet("streams/history")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetStreamHistoryViewModel))]
        public async Task<IActionResult> GetStreamHistory([FromQuery] string artistId, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            return Ok(await Mediator.Send(new GetStreamHistoryQuery { ArtistId = artistId, From = from, To = to }));
        }
    }
}
