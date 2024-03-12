using FluentAssertions;
using Moq;
using VideoClub.Contracts.Interfaces;
using VideoClub.Persistence.EF;
using VideoClub.Services.Genres;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Genres.Exceptions;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using Xunit;

namespace VideoClub.Services.Unit.Tests.GenreManagerServiceTests
{
    public class GenreManagerServiceDeleteTests
    {
        private readonly GenreManagerService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public GenreManagerServiceDeleteTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = GenreManagerServiceFactory.Create(_context);
        }
        [Fact]
        public async void Delete_delete_a_genre_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);

            await _sut.Delete(genre.Id);
        }

        [Fact]
        public async void Delete_delete_a_genre_properly_mock()
        {
            var genre = new GenreBuilder().Build();
            genre.Id = 1;
            var dto = UpdateGenreDtoFactory.Create();
            var repositoryMock = new Mock<GenreRepository>();
            var unitOfWorkMock = new Mock<UnitOfWork>();
            var dateTimeServiceMock = new Mock<DateTimeService>();
            dateTimeServiceMock.Setup(_ => _.Now()).Returns(new DateTime(2024, 11, 21));
            var sut = new GenreManagerAppService(repositoryMock.Object, unitOfWorkMock.Object, dateTimeServiceMock.Object);
            var sutMock = new Mock<GenreManagerAppService>();
            repositoryMock.Setup(_ => _.FindGenreById(genre.Id)).Returns(genre);

            await sut.Delete(genre.Id);

            unitOfWorkMock.Verify(_ => _.Complete());
        }
        [Fact]
        public async void Delete_throw_exception_when_genre_doesnt_exist()
        {
            var id = 312;

            var actual = () => _sut.Delete(id);

            await actual.Should().ThrowExactlyAsync<GenreNotFoundException>();
        }
    }
}
