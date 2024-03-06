using FluentAssertions;
using VideoClub.Persistence.EF;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.Unit;

namespace VideoClub.Services.Unit.Tests.GenreManagerServiceTests
{
    public class GenreManagerServiceGetTests
    {
        private readonly GenreManagerService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public GenreManagerServiceGetTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = GenreManagerServiceFactory.Create(_context);
        }
        [Fact]
        public async void Get_the_get_method_shows_the_count_properly()
        {
            var genre = new GenreBuilder().Build();
            var genre2 = new GenreBuilder().Build();
            var genre3 = new GenreBuilder().Build();
            var genre4 = new GenreBuilder().Build();
            _context.Save(genre);
            _context.Save(genre2);
            _context.Save(genre3);
            _context.Save(genre4);
            var excepted = 4;

            var actual = await _sut.Get();

            actual.Count.Should().Be(excepted);
        }
        [Fact]
        public async void Get_the_get_method_shows_the_genre_information_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);

            var actual = await _sut.Get();

            var result = actual.Single();
            result.Title.Should().Be(genre.Title);
        }
    }
}
