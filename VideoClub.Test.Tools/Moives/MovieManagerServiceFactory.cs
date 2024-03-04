using VideoClub.Persistence.EF;
using VideoClub.Persistence.EF.Movies;
using VideoClub.Services.Movies;
using VideoClub.Services.Movies.Contracts;

namespace VideoClub.Test.Tools.Moives
{
    public static class MovieManagerServiceFactory
    {
        public static MovieManagerService Create(EFDataContext context)
        {
            var repository = new EfMovieRepository(context);
            var unitOfWork = new EFUnitOfWork(context);
            return new MovieManagerAppService(repository, unitOfWork);
        }
    }
}
