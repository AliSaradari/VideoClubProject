using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using VideoClub.Contracts.Interfaces;
using VideoClub.Entities.Genres;
using VideoClub.Entities.Movies;
using VideoClub.Persistence.EF;
using VideoClub.Persistence.EF.Movies;
using VideoClub.Services.Genres.Contracts;
using VideoClub.Services.Genres.Exceptions;
using VideoClub.Services.Movies;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Services.Movies.Exceptions;
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
        //[Fact]
        //public async void Add_throw_exception_while_adding_new_movie_when_title_is_empty()
        //{
        //    var genre = new GenreBuilder().Build();
        //    _context.Save(genre);
        //    var dto = AddMovieDtoFactory.Create(genre.Id, " ");

        //    var actual = () => _sut.Add(dto);

        //    actual.Should().ThrowExactlyAsync<TitleCannotBeEmptyException>();
        //}
        //[Fact]
        //public async void Add_throw_exception_while_adding_new_movie_when_count_is_negative()
        //{
        //    var genre = new GenreBuilder().Build();
        //    _context.Save(genre);
        //    var dto = AddMovieDtoFactory.Create(genre.Id, count: -3);

        //    var actual = () => _sut.Add(dto);

        //    actual.Should().ThrowExactlyAsync<CountCannotBeNegativeException>();


        //}
        [Fact]
        public async void Add_add_a_new_movie_properly_moq()
        {
            var genre = new GenreBuilder().Build();
            _context.Save(genre);
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
