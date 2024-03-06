using Moq;
using VideoClub.Contracts.Interfaces;
using VideoClub.Persistence.EF;
using VideoClub.Persistence.EF.Movies;
using VideoClub.Services.Movies;
using VideoClub.Services.Movies.Contracts;

namespace VideoClub.Test.Tools.Moives
{
    public static class MovieManagerServiceFactory
    {
        public static MovieManagerService Create(EFDataContext context, DateTime? fakeTime = null)
        {
            var repository = new EfMovieRepository(context);
            var unitOfWork = new EFUnitOfWork(context);
            var dateTimeServiceMock = new Mock<DateTimeService>();
            dateTimeServiceMock.Setup(_ => _.Now()).Returns(fakeTime ?? new DateTime(2023,10,10));
            return new MovieManagerAppService(repository, unitOfWork, dateTimeServiceMock.Object);
        }
    }
}
