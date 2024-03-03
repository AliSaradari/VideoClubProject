using VideoClub.Contracts.Interfaces;
using VideoClub.Entities.Genres;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Genres.Contracts.Dtos;
using VideoClub.Services.Genres.Exceptions;

namespace VideoClub.Services.Genres
{
    public class GenreManagerAppService : GenreManagerService
    {
        private readonly GenreRepository _repository;
        private readonly UnitOfWork _unitOfWork;
        public GenreManagerAppService(
            GenreRepository repository,
            UnitOfWork unitOfWork
            )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(AddGenreDto dto)
        {
            var checkDuplicatedGenre = _repository.IsExistGenre(dto.Title);
            if (checkDuplicatedGenre == true)
            {
                throw new ThisGenreAlreadyIsExistException();
            }
            var genre = new Genre()
            {
                Title = dto.Title,
            };
            _repository.Add(genre);
            await _unitOfWork.Complete();
        }

        public async Task Delete(int id)
        {
            var genre = _repository.FindGenreById(id);
            if (genre == null)
            {
                throw new GenreNotFoundException();
            }
            _repository.Delete(genre);
            await _unitOfWork.Complete();
        }

        public async Task<List<GetGenreManagerDto>> Get()
        {
            return _repository.ManagerGet();
        }

        public async Task Update(int id, UpdateGenreDto dto)
        {
            var genre = _repository.FindGenreById(id);
            if (genre == null)
            {
                throw new GenreNotFoundException();
            }
            var checkDuplicatedGenre = _repository.IsExistGenre(dto.Title);
            if (checkDuplicatedGenre == true)
            {
                throw new ThisGenreAlreadyIsExistException();
            }
            genre.Title = dto.Title;
            await _unitOfWork.Complete();
        }
    }
}
