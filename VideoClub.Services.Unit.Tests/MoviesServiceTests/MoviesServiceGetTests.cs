using FluentAssertions;
using VideoClub.Persistence.EF;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using VideoClub.Test.Tools.Moives;

namespace VideoClub.Services.Unit.Tests.MoviesServiceTests
{
    public class MoviesServiceGetTests
    {
        private readonly MovieService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public MoviesServiceGetTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = MovieServiceFactory.Create(_context);
        }
        [Fact]
        public async void Get_the_get_method_shows_the_count_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var movie = new MovieBuilder(genre.Id).Build();
            var movie2 = new MovieBuilder(genre.Id).Build();
            _context.Save(movie);
            _context.Save(movie2);
            var filterDto = new GetMovieFilterDtoBuilder().Build();
            var excepted = 2;

            var actual = await _sut.Get(filterDto);

            actual.Count.Should().Be(excepted);
        }
        [Fact]
        public async void Get_the_get_method_shows_the_movie_information_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var movie = new MovieBuilder(genre.Id).Build();
            _context.Save(movie);
            var filtedDto = new GetMovieFilterDtoBuilder().Build();

            var actual = await _sut.Get(filtedDto);

            var result = actual.Single();
            result.Title.Should().Be(movie.Title);
            result.Description.Should().Be(movie.Description);
            result.PublishYear.Should().Be(movie.PublishYear);
            result.Director.Should().Be(movie.Director);
            result.Duration.Should().Be(movie.Duration);
            result.GenreId.Should().Be(movie.GenreId);
            result.MinimumAllowedAge.Should().Be(movie.MinimumAllowedAge);
            result.DailyRentalPrice.Should().Be(movie.DailyRentalPrice);
            result.PenaltyRates.Should().Be(movie.PenaltyRates);
            result.Count.Should().Be(movie.Count);
        }
        [Theory]
        [InlineData("Interstaller", "Interstaller")]
        [InlineData("Interstaller", "Inters")]
        public async Task Get_gets_movies_filtered_by_title(string name, string filter)
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
            var filterDto = new GetMovieFilterDtoBuilder().WithTitle(filter)
                .Build();
            var expected = 1;

            var result = await _sut.Get(filterDto);

            result.Count.Should().Be(expected);
            //result.Single().Id.Should().Be(film.Id);
        }
    }
}
