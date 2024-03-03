using VideoClub.Entities.Genres;
using VideoClub.Services.Genres.Contracts.Dtos;

namespace VideoClub.Services.Genres.Contracts
{
    public interface GenreRepository
    {
        void Add(Genre genre);
        void Delete(Genre genre);
        Genre FindGenreById(int id);
        List<GetGenreManagerDto> ManagerGet();
        bool IsExistGenre(string title);
        Task<List<GetGenreDto>> Get();
    }
}
