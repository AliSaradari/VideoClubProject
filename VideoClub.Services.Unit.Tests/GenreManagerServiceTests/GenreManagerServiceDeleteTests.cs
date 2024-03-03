using FluentAssertions;
using VideoClub.Persistence.EF;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Genres.Exceptions;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.Unit;

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
        public async void Delete_throw_exception_when_genre_doesnt_exist()
        {
            var id = 312;

            var actual = () => _sut.Delete(id);

            actual.Should().ThrowExactlyAsync<GenreNotFoundException>();
        }
    }
}
