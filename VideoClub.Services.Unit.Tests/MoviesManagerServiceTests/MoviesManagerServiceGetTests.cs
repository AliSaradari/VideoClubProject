using FluentAssertions;
using System.Reflection.Emit;
using VideoClub.Persistence.EF;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using VideoClub.Test.Tools.Moives;
using Xunit;

namespace VideoClub.Services.Unit.Tests.MoviesManagerServiceTests
{
    public class MoviesManagerServiceGetTests
    {
        private readonly MovieManagerService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public MoviesManagerServiceGetTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = MovieManagerServiceFactory.Create(_context);
        }
        [Fact]
        public async void Get_the_get_method_shows_the_count_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var movie = new MovieBuilder(genre.Id).Build();
            var movie2 = new MovieBuilder(genre.Id).Build();
            var movie3 = new MovieBuilder(genre.Id).Build();
            _context.Save(movie);
            _context.Save(movie2);
            _context.Save(movie3);
            var filtedDto = new GetMovieManagerFilterDtoBuilder().Build();
            var excepted = 3;

            var actual = await _sut.Get(filtedDto);

            actual.Count.Should().Be(excepted);
        }
        [Fact]
        public async void Get_the_get_method_shows_the_movie_information_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var movie = new MovieBuilder(genre.Id).Build();
            _context.Save(movie);
            var filtedDto = new GetMovieManagerFilterDtoBuilder().Build();

            await _sut.Get(filtedDto);

            var actual = _readContext.Movies.Single();
            actual.Title.Should().Be(movie.Title);
            actual.Description.Should().Be(movie.Description);
            actual.PublishYear.Should().Be(movie.PublishYear);
            actual.Director.Should().Be(movie.Director);
            actual.Duration.Should().Be(movie.Duration);
            actual.GenreId.Should().Be(movie.GenreId);
            actual.MinimumAllowedAge.Should().Be(movie.MinimumAllowedAge);
            actual.DailyRentalPrice.Should().Be(movie.DailyRentalPrice);
            actual.PenaltyRates.Should().Be(movie.PenaltyRates);
            actual.Count.Should().Be(movie.Count);
        }

        [Theory]
        [InlineData("Interstaller", "Interstaller")]
        public async Task Get_gets_films_filtered_by_name(string name, string filter)
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var film = new MovieBuilder(genre.Id)
                .WithTitle(name)
                .WithGenreId(genre.Id).Build();
            _context.Save(film);
            var film2 = new MovieBuilder(genre.Id)
               .WithGenreId(genre.Id).Build();
            _context.Save(film2);
            var filterDto = new GetMovieManagerFilterDtoBuilder().WithTitle(filter)
                .Build();
            var expected = 1;

            var result = await _sut.Get(filterDto);

            result.Count.Should().Be(expected);
            result.Single().Id.Should().Be(film.Id);
        }
    }
}
