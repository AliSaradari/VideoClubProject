using Microsoft.EntityFrameworkCore;
using VideoClub.Entities.Genres;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Genres.Contracts.Dtos;

namespace VideoClub.Persistence.EF.Genres
{
    public class EfGenreRepository : GenreRepository
    {
        private readonly DbSet<Genre> _genres;
        public EfGenreRepository(EFDataContext context)
        {
            _genres = context.Genres;
        }
        public void Add(Genre genre)
        {
            _genres.Add(genre);
        }

        public void Delete(Genre genre)
        {
            _genres.Remove(genre);
        }

        public Genre FindGenreById(int id)
        {
            return _genres.FirstOrDefault(_ => _.Id == id);
        }

        public List<GetGenreManagerDto> ManagerGet()
        {
            return _genres.Select(g => new GetGenreManagerDto
            {
                Id = g.Id,
                Title = g.Title,
                Rate = g.Rate,
            }).ToList();
        }

        public bool IsExistGenre(string title)
        {
            return _genres.Any(_ => _.Title == title);
        }

        public Task<List<GetGenreDto>> Get()
        {
            return _genres.Select(g => new GetGenreDto()
            {
                Title = g.Title,
                Rate = g.Rate,
            }).ToListAsync();
        }
    }
}
