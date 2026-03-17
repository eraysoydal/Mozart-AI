using CleanArchitecture.Core.Features.Genres.Commands.CreateGenre;
using CleanArchitecture.Core.Features.Genres.Queries.GetAllGenres;
using CleanArchitecture.Core.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    public class GenreController : BaseApiController
    {

        [HttpPost]
        public async Task<IActionResult> Post(CreateGenreCommand command)
        {
            return Ok(await Mediator.Send(command));
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<GetAllGenresViewModel>))]
        public async Task<PagedResponse<GetAllGenresViewModel>> Get([FromQuery] GetAllGenresParameter filter)
        {
            return await Mediator.Send(new GetAllGenresQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber });
        }
    }
}
