using FluentAssertions;
using VideoClub.Persistence.EF;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using VideoClub.Test.Tools.Moives;

namespace VideoClub.Services.Unit.Tests.MoviesManagerServiceTests
{
    public class MoviesManagerServiceAddTests
    {
        private readonly MovieManagerService _sut;
        private readonly EFDataContext _context;
        private readonly EFDataContext _readContext;
        public MoviesManagerServiceAddTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _sut = MovieManagerServiceFactory.Create(_context);
        }
        [Fact]
        public async void Add_add_a_new_movie_properly()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var dto = AddMovieDtoFactory.Create(genre.Id);

            await _sut.Add(dto);

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
    }

}
