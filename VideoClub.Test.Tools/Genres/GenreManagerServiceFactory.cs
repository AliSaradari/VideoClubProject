using Moq;
using System;
using VideoClub.Contracts.Interfaces;
using VideoClub.Persistence.EF;
using VideoClub.Persistence.EF.Genres;
using VideoClub.Services.Genres;
using VideoClub.Services.Genres.Contracts;

namespace VideoClub.Test.Tools.Genres
{
    public static class GenreManagerServiceFactory
    {
        public static GenreManagerService Create(EFDataContext context,DateTime? fakeDateTime = null)
        {
            var repository = new EfGenreRepository(context);
            var unitOfWork = new EFUnitOfWork(context);
            var dateTimeServiceMock = new Mock<DateTimeService>();
            dateTimeServiceMock.Setup(_ => _.Now()).Returns(fakeDateTime ?? new DateTime(2024,05,12));
            return new GenreManagerAppService(repository,unitOfWork,dateTimeServiceMock.Object);
        }
    }
}
