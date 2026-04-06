using CleanArchitecture.Core.Features.Users.Commands.DeleteUser;
using CleanArchitecture.Core.Features.Users.Commands.UpdateUser;
using CleanArchitecture.Core.Features.Users.Queries.GetAllArtists;
using CleanArchitecture.Core.Features.Users.Queries.GetAllUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class UserController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllUsersParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllUsersQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET: api/<controller>/artists
        [HttpGet("artists")]
        public async Task<IActionResult> GetArtists([FromQuery] GetAllArtistsParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllArtistsQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // PUT api/<controller>/{id}
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(string id, UpdateUserCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/{id}
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await Mediator.Send(new DeleteUserCommand { Id = id }));
        }
    }
}
