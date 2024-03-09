using Microsoft.AspNetCore.Mvc;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Services.Movies.Contracts.Dtos;

namespace VideoClub.Api.Controllers.Movies
{
    [Route("api/moviesManager")]
    [ApiController]
    public class MovieManagerController : ControllerBase
    {
        private readonly MovieManagerService _service;
        public MovieManagerController(MovieManagerService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add([FromBody]AddMovieDto dto)
        {
            await _service.Add(dto);
        }
        [HttpPatch("{id}")]
        public async Task Update([FromRoute] int id, [FromBody] UpdateMovieDto dto)
        {
            await _service.Update(id, dto);
        }
        [HttpGet]
        public async Task<List<GetMovieManagerDto>> Get([FromQuery] GetMovieManagerFilterDto dto)
        {
            return await _service.Get(dto);
        }
        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _service.Delete(id);
        }
    }
}
