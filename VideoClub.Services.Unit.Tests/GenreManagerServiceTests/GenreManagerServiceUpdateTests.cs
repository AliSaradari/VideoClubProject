using FluentAssertions;
using VideoClub.Persistence.EF;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Genres.Exceptions;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.Unit;

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
        }
        [Fact]
        public async void Update_throw_exception_when_genre_doesnt_exist()
        {
            var id = 1234;
            var dto = UpdateGenreDtoFactory.Create();

            var actual = () => _sut.Update(id, dto);

            actual.Should().ThrowExactlyAsync<GenreNotFoundException>();
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

            actual.Should().ThrowExactlyAsync<ThisGenreAlreadyIsExistException>();
        }
    }
}
