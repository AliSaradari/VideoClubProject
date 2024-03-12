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
    public class GenreManagerServiceUpdateTests
    {
        private readonly GenreManagerService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public GenreManagerServiceUpdateTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = GenreManagerServiceFactory.Create(_context);
        }
        [Fact]
        public async void Update_update_a_genre_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var dto = UpdateGenreDtoFactory.Create();

            await _sut.Update(genre.Id, dto);

            var actual = _readContext.Genres.Single();
            actual.Title.Should().Be(dto.Title);
        }
        [Fact]
        public async void Update_update_a_genre_properly_mock()
        {
            var genre = new GenreBuilder().Build();
            genre.Id = 1;
            var dto = UpdateGenreDtoFactory.Create();
            var repositoryMock = new Mock<GenreRepository>();
            var unitOfWorkMock = new Mock<UnitOfWork>();
            var dateTimeServiceMock = new Mock<DateTimeService>();
            dateTimeServiceMock.Setup(_ => _.Now()).Returns(new DateTime(2024, 11, 21));
            var sut = new GenreManagerAppService(repositoryMock.Object, unitOfWorkMock.Object, dateTimeServiceMock.Object);
            repositoryMock.Setup(_ => _.FindGenreById(genre.Id)).Returns(genre);

            await sut.Update(genre.Id, dto);

            unitOfWorkMock.Verify(_ => _.Complete(),Times.Once);            


        }
        [Fact]
        public async void Update_throw_exception_when_genre_doesnt_exist()
        {
            var id = 1234;
            var dto = UpdateGenreDtoFactory.Create();

            var actual = () => _sut.Update(id, dto);

            await actual.Should().ThrowExactlyAsync<GenreNotFoundException>();
        }
        [Fact]
        public async void Update_throw_exception_when_updated_genre_is_duplicated()
        {
            var title = "same-dummy-title";
            var genre = new GenreBuilder()
                .WithTitle(title)
                .Build();
            _context.Save(genre);
            var dto = UpdateGenreDtoFactory.Create(title);

            var actual = () => _sut.Update(genre.Id, dto);

            await actual.Should().ThrowExactlyAsync<ThisGenreAlreadyIsExistException>();
        }
    }
}
