using VideoClub.Entities.Genres;
using VideoClub.Services.Genres.Contracts.Dtos;

namespace VideoClub.Services.Genres.Contracts
{
    public interface GenreRepository
    {
        void Add(Genre genre);
        void Delete(Genre genre);
        void Update(Genre genre); 
        Genre FindGenreById(int id);
        List<GetGenreManagerDto> ManagerGet(GetGenreManagerFilterDto filterDto);
        bool IsExistGenre(string title);
        List<GetGenreDto> Get(GetGenreFilterDto dto);
    }
}
