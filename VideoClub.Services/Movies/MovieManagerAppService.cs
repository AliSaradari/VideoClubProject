using VideoClub.Contracts.Interfaces;
using VideoClub.Entities.Movies;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Genres.Exceptions;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Services.Movies.Contracts.Dtos;
using VideoClub.Services.Movies.Exceptions;

namespace VideoClub.Services.Movies
{
    public class MovieManagerAppService : MovieManagerService
    {
        private readonly MovieRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        private readonly DateTimeService _dateTimeService;
        private readonly GenreRepository _genreRepository;

        public MovieManagerAppService(
            MovieRepository repository,
            UnitOfWork unitOfWork,
            DateTimeService dateTimeService,
            GenreRepository genreRepository
           )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _dateTimeService = dateTimeService;
            _genreRepository = genreRepository;
        }

        public async Task Add(AddMovieDto dto)
        {
            var IsExistGenre = _genreRepository.FindGenreById(dto.GenreId);
            if (IsExistGenre == null)
            {
                throw new GenreNotFoundException();
            }
            if (dto.Count < 0)
            {
                throw new CountCannotBeNegativeException();
            }
            if (string.IsNullOrWhiteSpace(dto.Title))
            {
                throw new TitleCannotBeEmptyException();
            }
            var checkDuplicateTitle = _repository.IsExistMovie(dto.Title);
            if(checkDuplicateTitle == true)
            {
                throw new TitleCannotBeDuplicateException();
            }
            var movie = new Movie()
            {
                Title = dto.Title,
                Description = dto.Description,
                PublishYear = dto.PublishYear,
                Director = dto.Director,
                Duration = dto.Duration,
                GenreId = dto.GenreId,
                MinimumAllowedAge = dto.MinimumAllowedAge,
                DailyRentalPrice = dto.DailyRentalPrice,
                PenaltyRates = dto.PenaltyRates,
                Count = dto.Count,
                CreateAt = _dateTimeService.Now()
            };
            _repository.Add(movie);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var movie = _repository.FindById(id);
            if (movie == null)
            {
                throw new MovieNotFoundException();
            }
            _repository.Delete(movie);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetMovieManagerDto>> Get(GetMovieManagerFilterDto filterDto)
        {
            return _repository.ManagerGet(filterDto);
        }

        public async Task Update(int id, UpdateMovieDto dto)
        {
            var movie = _repository.FindById(id);
            if (movie == null)
            {
                throw new MovieNotFoundException();
            }
            movie.Title = dto.Title;
            movie.Description = dto.Description;
            movie.PublishYear = dto.PublishYear;
            movie.Director = dto.Director;
            movie.Duration = dto.Duration;
            movie.GenreId = dto.GenreId;
            movie.MinimumAllowedAge = dto.MinimumAllowedAge;
            movie.DailyRentalPrice = dto.DailyRentalPrice;
            movie.PenaltyRates = dto.PenaltyRates;
            movie.Count = dto.Count;
            await _unitOfWork.Complete();
        }
    }
}
