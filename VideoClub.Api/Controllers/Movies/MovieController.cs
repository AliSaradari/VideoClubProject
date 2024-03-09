using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Services.Movies.Contracts.Dtos;

namespace VideoClub.Api.Controllers.Movies
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _service;
        public MovieController(MovieService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<List<GetMovieDto>> Get([FromQuery]GetMovieFilterDto dto)
        {
           return await _service.Get(dto);
        }
    }
}
