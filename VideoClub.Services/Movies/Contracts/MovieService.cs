using VideoClub.Services.Movies.Contracts.Dtos;

namespace VideoClub.Services.Movies.Contracts
{
    public interface MovieService
    {
        Task<List<GetMovieDto>> Get(GetMovieFilterDto dto);
    }
}
