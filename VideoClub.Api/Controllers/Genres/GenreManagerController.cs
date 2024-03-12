using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Genres.Contracts.Dtos;

namespace VideoClub.Api.Controllers.Genres
{
    [Route("api/genresManager")]
    [ApiController]
    public class GenreManagerController : ControllerBase
    {
        private readonly GenreManagerService _service;

        public GenreManagerController(GenreManagerService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task Add([Required][FromQuery] AddGenreDto dto)
        {
            await _service.Add(dto);
        }
        [HttpPatch("{id}")]
        public async Task Update([FromRoute] int id, [FromBody] UpdateGenreDto dto)
        {
            await _service.Update(id, dto);
        }
        [HttpGet]
        public async Task<List<GetGenreManagerDto>> Get([FromQuery] GetGenreManagerFilterDto dto)
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
