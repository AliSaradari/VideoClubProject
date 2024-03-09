using VideoClub.Persistence.EF.Movies;
using VideoClub.Persistence.EF;
using VideoClub.Services.Movies;
using VideoClub.Services.Movies.Contracts;

namespace VideoClub.Test.Tools.Moives
{
    public static class MovieServiceFactory
    {
        public static MovieService Create(EFDataContext context)
        {
            var repository = new EfMovieRepository(context);
            var unitOfWork = new EFUnitOfWork(context);
            return new MovieAppService(repository,unitOfWork);
        }
    }
}
