using FluentAssertions;
using Moq;
using System;
using VideoClub.Contracts.Interfaces;
using VideoClub.Entities.Movies;
using VideoClub.Persistence.EF;
using VideoClub.Persistence.EF.Movies;
using VideoClub.Services.Movies;
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
        private readonly DateTime _fakeDate;
        public MoviesManagerServiceAddTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readContext = db.CreateDataContext<EFDataContext>();
            _fakeDate = new DateTime(2023, 10, 10);
            _sut = MovieManagerServiceFactory.Create(_context, _fakeDate);
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
            actual.CreatedAt.Should().Be(_fakeDate);
        }

        [Fact]
        public async void Add_add_a_new_movie_properly_moq()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
            var dto = AddMovieDtoFactory.Create(genre.Id);

            var repositoryMock = new Mock<MovieRepository>();
            var unitOfWorkMock = new Mock<UnitOfWork>();
            var dateTimeServiceMock = new Mock<DateTimeService>();
            dateTimeServiceMock.Setup(_ => _.Now()).Returns(new DateTime(2023, 10, 10));
            var sut = new MovieManagerAppService(repositoryMock.Object, unitOfWorkMock.Object, dateTimeServiceMock.Object);

            await sut.Add(dto);

           repositoryMock.Verify(_=>_.Add(It.IsAny<Movie>()), Times.Once);
            unitOfWorkMock.Verify(_=>_.Complete(), Times.Once);
        }
    }

}
