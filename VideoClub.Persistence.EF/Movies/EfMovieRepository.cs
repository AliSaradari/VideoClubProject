using Microsoft.EntityFrameworkCore;
using VideoClub.Entities.Movies;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Services.Movies.Contracts.Dtos;

namespace VideoClub.Persistence.EF.Movies
{
    public class EfMovieRepository : MovieRepository
    {
        private readonly DbSet<Movie> _movies;
        public EfMovieRepository(EFDataContext context)
        {
            _movies = context.Movies;
        }

        public void Add(Movie movie)
        {
            _movies.Add(movie);
        }

        public void Delete(Movie movie)
        {
            _movies.Remove(movie);
        }

        public Movie FindById(int id)
        {
            return _movies.FirstOrDefault(_ => _.Id == id);
        }

        public List<GetMovieDto> Get(GetMovieFilterDto dto)
        {
            var result = _movies.Select(m => new GetMovieDto()
            {
                Title = m.Title,
                Description = m.Description,
                PublishYear = m.PublishYear,
                Director = m.Director,
                Duration = m.Duration,
                GenreId = m.GenreId,
                MinimumAllowedAge = m.MinimumAllowedAge,
                DailyRentalPrice = m.DailyRentalPrice,
                PenaltyRates = m.PenaltyRates,
                Count = m.Count,
            });
            if (dto.Title != null)
            {
                result = result.Where(m => m.Title.Replace(" ", string.Empty).ToLower().Contains(dto.Title.Replace(" ", string.Empty).ToLower()));
            }
            return result.ToList();
        }

        public List<GetMovieManagerDto> ManagerGet(GetMovieManagerFilterDto dto)
        {
            var result = _movies.Select(m => new GetMovieManagerDto()
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                PublishYear = m.PublishYear,
                Director = m.Director,
                Duration = m.Duration,
                GenreId = m.GenreId,
                MinimumAllowedAge = m.MinimumAllowedAge,
                DailyRentalPrice = m.DailyRentalPrice,
                PenaltyRates = m.PenaltyRates,
                Count = m.Count,
            });
            if(dto.Title != null)
            {
                result = result.Where(m => m.Title.Replace(" ", string.Empty).ToLower().Contains(dto.Title.Replace(" ",string.Empty).ToLower()));
            }
            return result.ToList();
        }
    }
}
