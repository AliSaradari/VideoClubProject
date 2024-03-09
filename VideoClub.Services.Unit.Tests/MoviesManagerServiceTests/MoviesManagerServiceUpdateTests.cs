using FluentAssertions;
using VideoClub.Persistence.EF;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Services.Movies.Exceptions;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using VideoClub.Test.Tools.Moives;

namespace VideoClub.Services.Unit.Tests.MoviesManagerServiceTests
{
    public class MoviesManagerServiceUpdateTests
    {
        private readonly MovieManagerService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public MoviesManagerServiceUpdateTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = MovieManagerServiceFactory.Create(_context);
        }
        [Fact]
        public async void Update_update_a_movie_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var movie = new MovieBuilder(genre.Id).Build();
            _context.Save(movie);
            var genre2 = new GenreBuilder().Build();
            _context.Save(genre2);
            var dto = UpdateMovieDtoFactory.Create(genre2.Id);

            await _sut.Update(movie.Id, dto);

            var actual = _readContext.Movies.Single();
            actual.Title.Should().Be(dto.Title);
            actual.Description.Should().Be(dto.Description);
            actual.PublishYear.Should().Be(dto.PublishYear);
            actual.Director.Should().Be(dto.Director);
            actual.Duration.Should().Be(dto.Duration);
            actual.GenreId.Should().Be(dto.GenreId);
            actual.MinimumAllowedAge.Should().Be(dto.MinimumAllowedAge);
            actual.DailyRentalPrice.Should().Be(dto.DailyRentalPrice);
            actual.PenaltyRates.Should().Be(dto.PenaltyRates);
            actual.Count.Should().Be(dto.Count);

        }
        [Fact]
        public async void Update_throw_exception_when_movie_doesnt_exist()
        {
            var dummyId = 124;
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var dto = UpdateMovieDtoFactory.Create(genre.Id);

            var actual = () => _sut.Update(dummyId, dto);

            await actual.Should().ThrowExactlyAsync<MovieNotFoundException>();
        }
    }
}
