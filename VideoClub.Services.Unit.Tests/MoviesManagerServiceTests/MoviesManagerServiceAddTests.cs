using FluentAssertions;
using Moq;
using VideoClub.Contracts.Interfaces;
using VideoClub.Entities.Movies;
using VideoClub.Persistence.EF;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Genres.Exceptions;
using VideoClub.Services.Movies;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Services.Movies.Exceptions;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.Unit;
using VideoClub.Test.Tools.Moives;
using Xunit;

namespace VideoClub.Services.Unit.Tests.MoviesManagerServiceTests
{
    public class MoviesManagerServiceAddTests : BusinessUnitTest
    {
        private readonly MovieManagerService _sut;
        private readonly DateTime _fakeDate;
        public MoviesManagerServiceAddTests()
        {
            _fakeDate = new DateTime(2023, 10, 10);
            _sut = MovieManagerServiceFactory.Create(SetupContext, _fakeDate);
        }
        [Fact]
        public async void Add_add_a_new_movie_properly()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var dto = AddMovieDtoFactory.Create(genre.Id);

            await _sut.Add(dto);

            var actual = ReadContext.Movies.Single();
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
            actual.CreateAt.Should().Be(_fakeDate);
        }
        [Fact]
        public async void Add_throw_exception_when_genre_doesnt_exist()
        {
            var dummuGenreId = 12;
            var dto = AddMovieDtoFactory.Create(dummuGenreId);

            var actual = () => _sut.Add(dto);

            actual.Should().ThrowExactlyAsync<GenreNotFoundException>();
        }
        [Fact]
        public async void Add_throw_exception_while_adding_new_movie_when_title_is_empty()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var dto = AddMovieDtoFactory.Create(genre.Id, " ");

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<TitleCannotBeEmptyException>();
        }
        [Fact]
        public async void Add_throw_exception_while_adding_new_movie_when_count_is_negative()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var dto = AddMovieDtoFactory.Create(genre.Id, count: -3);

            var actual = () => _sut.Add(dto);

            await actual.Should().ThrowExactlyAsync<CountCannotBeNegativeException>();


        }
        [Fact]
        public async void Add_add_a_new_movie_properly_moq()
        {
            var genre = new GenreBuilder().Build();
            DbContext.Save(genre);
            var dto = AddMovieDtoFactory.Create(genre.Id);

            var repositoryMock = new Mock<MovieRepository>();
            var genreRepositoryMock = new Mock<GenreRepository>();
            var unitOfWorkMock = new Mock<UnitOfWork>();
            var dateTimeServiceMock = new Mock<DateTimeService>();
            dateTimeServiceMock.Setup(_ => _.Now()).Returns(new DateTime(2023, 10, 10));
            var sut = new MovieManagerAppService(repositoryMock.Object, unitOfWorkMock.Object, dateTimeServiceMock.Object, genreRepositoryMock.Object);
            genreRepositoryMock.Setup(_ => _.FindGenreById(genre.Id)).Returns(genre);

            await sut.Add(dto);

            repositoryMock.Verify(_ => _.Add(It.IsAny<Movie>()), Times.Once);
            unitOfWorkMock.Verify(_ => _.Complete(), Times.Once);
        }
    }

}
