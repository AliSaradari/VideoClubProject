using VideoClub.Contracts.Interfaces;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Genres.Contracts.Dtos;

namespace VideoClub.Services.Genres
{
    public class GenreAppService : GenreService
    {
        private readonly GenreRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        public GenreAppService(
            GenreRepository repository,
            UnitOfWork unitOfWork
            )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public Task<List<GetGenreDto>> Get()
        {
           return _repository.Get();
        }
    }
}
