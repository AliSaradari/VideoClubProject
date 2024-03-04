using VideoClub.Services.Movies.Contracts.Dtos;

namespace VideoClub.Services.Movies.Contracts
{
    public interface MovieManagerService
    {
        Task Add(AddMovieDto dto);
        Task Delete(int id);
        Task<List<GetMovieManagerDto>> Get(GetMovieManagerFilterDto filterDto);
        Task Update(int id, UpdateMovieDto dto);
    }
}
