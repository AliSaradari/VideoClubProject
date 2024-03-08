using FluentAssertions;
using VideoClub.Persistence.EF;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Genres.Contracts.Dtos;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.Unit;

namespace VideoClub.Services.Unit.Tests.GenreServiceTests
{
    public class GenreServiceGetTests
    {
        private readonly GenreService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public GenreServiceGetTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = GenreServiceFactory.Create(_context);
        }
        [Fact]
        public async void Get_the_get_method_shows_the_count_properly()
        {
            var genre = new GenreBuilder().Build();
            var genre2 = new GenreBuilder().Build();
            _context.Save(genre);
            _context.Save(genre2);
            var excepted = 2;
            var filterDto = new GetGenreFilterDto()
            {
                Title = null
            };

            var actual = await _sut.Get(filterDto);

            actual.Count.Should().Be(excepted);
        }
        [Fact]
        public async void Get_the_get_method_shows_the_genre_information_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var filterDto = new GetGenreFilterDto()
            {
                Title = null
            };

            var actual = await _sut.Get(filterDto);

            var result = actual.Single();
            result.Title.Should().Be(genre.Title);
        }
    }
}
