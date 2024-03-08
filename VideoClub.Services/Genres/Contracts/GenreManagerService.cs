using VideoClub.Services.Genres.Contracts.Dtos;

namespace VideoClub.Services.Genres.Contracts
{
    public interface GenreManagerService
    {
        Task Add(AddGenreDto dto);
        Task Delete(int id);
        Task<List<GetGenreManagerDto>> Get(GetGenreManagerFilterDto filterDto);
        Task Update(int id, UpdateGenreDto dto);
    }
}
