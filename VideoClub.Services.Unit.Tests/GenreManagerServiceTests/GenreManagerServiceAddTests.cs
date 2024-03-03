using FluentAssertions;
using VideoClub.Persistence.EF;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Genres.Exceptions;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.Unit;

namespace VideoClub.Services.Unit.Tests.GenreManagerServiceTests
{
    public class GenreManagerServiceAddTests
    {
        private readonly GenreManagerService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public GenreManagerServiceAddTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = GenreManagerServiceFactory.Create(_context);
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
