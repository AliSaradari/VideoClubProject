using FluentAssertions;
using VideoClub.Entities.Genres;
using VideoClub.Entities.Movies;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Services.Movies.Exceptions;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using VideoClub.Test.Tools.Moives;
using Xunit;

namespace VideoClub.Spec.Tests.Movies
{
    [Scenario("عدم ثبت فیلم جدید با عنوان تکراری")]
    [Story("",
        AsA = "مدیر کلاب",
        InOrderTo = "فیلم جدید اضافه کنم",
        IWantTo = "تا بتوانم ان ها را اجاره دهم ")]
    public class FailToAddMovieWhenTitleIsDuplicated : BusinessIntegrationTest
    {
        private readonly MovieManagerService _sut;
        private Genre _genre;
        private Movie _movie;
        private Func<Task> _actual;
        public FailToAddMovieWhenTitleIsDuplicated()
        {
            _sut = MovieManagerServiceFactory.Create(SetupContext);
        }
        [Given("یک ژانر در فهرست ژانرها داریم")]
        [And("یک فیلم با عنوان جومانجی در فهرست فیلم ها داریم")]
        public void Given()
        {
            _genre = new GenreBuilder().Build();
            DbContext.Save(_genre);
            _movie = new MovieBuilder(_genre.Id)
                .WithTitle("جومانجی")
                .Build();
            DbContext.Save(_movie);
        }
        [When("یک فیلم با عنوان جومانجی اضافه میکنم")]
        public void When()
        {
            var dto = AddMovieDtoFactory.Create(_genre.Id, "جومانجی");
            _actual = () => _sut.Add(dto);
        }
        [Then("تنها یک فیلم با عنوان جومانجی در فهرست فیلم ها وجود دارد")]
        [And("و خطایی به کاربر نشان داد میشود")]
        public async Task Then()
        {
           await _actual.Should().ThrowExactlyAsync<TitleCannotBeDuplicateException>();
        }
        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When(),
                _ => Then().Wait());
        }

    }
}
