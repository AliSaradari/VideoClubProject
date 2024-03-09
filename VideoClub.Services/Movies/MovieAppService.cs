using VideoClub.Contracts.Interfaces;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Services.Movies.Contracts.Dtos;

namespace VideoClub.Services.Movies
{
    public class MovieAppService : MovieService
    {
        private readonly MovieRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public MovieAppService(
            MovieRepository repository,
            UnitOfWork unitOfWork
           )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetMovieDto>> Get(GetMovieFilterDto dto)
        {
            return  _repository.Get(dto);
        }
    }
}
