using FluentAssertions;
using Moq;
using VideoClub.Contracts.Interfaces;
using VideoClub.Entities.Genres;
using VideoClub.Persistence.EF;
using VideoClub.Services.Genres;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Genres.Exceptions;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.Unit;

namespace VideoClub.Services.Unit.Tests.GenreManagerServiceTests
{
    public class GenreManagerServiceAddTests
    {
        private readonly GenreManagerService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        private readonly DateTime _fakeDateTime;
        public GenreManagerServiceAddTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _fakeDateTime = new DateTime(2024, 11, 20);
            _sut = GenreManagerServiceFactory.Create(_context,_fakeDateTime);
        }
        [Fact]
        public async void Add_add_a_new_genre_properly()
        {
            var dto = AddGenreDtoFactory.Create();

            await _sut.Add(dto);

            var actual = _readContext.Genres.Single();
            actual.Title.Should().Be(dto.Title);
        }
        [Fact]
        public async void Add_add_a_new_genre_properly_mock()
        {
            var dto = AddGenreDtoFactory.Create();
            var repositoryMock = new Mock<GenreRepository>();
            var unitOfWorkMock = new Mock<UnitOfWork>();
            var dateTimeServiceMock = new Mock<DateTimeService>();
            dateTimeServiceMock.Setup(_ => _.Now()).Returns(new DateTime(2024, 11, 21));
            var sut = new GenreManagerAppService(repositoryMock.Object,unitOfWorkMock.Object,dateTimeServiceMock.Object);

            await sut.Add(dto);

            repositoryMock.Verify(_ => _.Add(It.Is<Genre>(_ => _.Title == dto.Title)), Times.Once);
            unitOfWorkMock.Verify(_ => _.Complete(),Times.Once);
        }
        [Fact]
        public async void Add_throw_exeption_when_genre_is_duplicated()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var dto = AddGenreDtoFactory.Create();

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<ThisGenreAlreadyIsExistException>();
        }
    }
}
