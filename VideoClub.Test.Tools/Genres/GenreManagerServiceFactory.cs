using VideoClub.Persistence.EF;
using VideoClub.Persistence.EF.Genres;
using VideoClub.Services.Genres;
using VideoClub.Services.Genres.Contracts;

namespace VideoClub.Test.Tools.Genres
{
    public static class GenreManagerServiceFactory
    {
        public static GenreManagerService Create(EFDataContext context)
        {
            var repository = new EfGenreRepository(context);
            var unitOfWork = new EFUnitOfWork(context);
            return new GenreManagerAppService(repository,unitOfWork);
        }
    }
}
