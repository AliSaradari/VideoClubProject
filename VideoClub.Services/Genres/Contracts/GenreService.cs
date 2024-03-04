using VideoClub.Services.Genres.Contracts.Dtos;

namespace VideoClub.Services.Genres.Contracts
{
    public interface GenreService
    {
        Task<List<GetGenreDto>> Get();
    }
}
