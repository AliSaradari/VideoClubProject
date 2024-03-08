using Microsoft.AspNetCore.Mvc;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Genres.Contracts.Dtos;

namespace VideoClub.Api.Controllers.Genres
{
    [Route("api/genres")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly GenreService _service;
        public GenreController(GenreService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<List<GetGenreDto>> Get([FromBody] GetGenreFilterDto dto )
        {
            return await _service.Get(dto);
        }
    }
}
