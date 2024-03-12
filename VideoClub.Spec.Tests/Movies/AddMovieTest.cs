using FluentAssertions;
using VideoClub.Entities.Genres;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Services.Movies.Contracts.Dtos;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using VideoClub.Test.Tools.Moives;
using Xunit;

namespace VideoClub.Spec.Tests.Movies
{
    [Scenario("اضافه شدن فیلم")]
    [Story("",
        AsA = "مدیر کلاب",
        InOrderTo = "فیلم جدید اضافه کنم",
        IWantTo = "تا بتوانم ان ها را اجاره دهم ")]
    public class AddMovieTest : BusinessIntegrationTest
    {
        private readonly MovieManagerService _sut;
        private Genre _genre;
        public AddMovieTest()
        {
            _sut = MovieManagerServiceFactory.Create(SetupContext);
        }
        [Given("هیچ فیلمی در آرشیو فیلم ها وحود ندارد")]
        [And("یک ژانر با عنوان تخیلی در فهرست ژانر ها وجود دارد")]
        private void Given()
        {
            _genre = new GenreBuilder()
                .WithTitle("Fantasy ")
                .Build();
            DbContext.Save(_genre);
        }
        [Then(" یک فیلم با عنوان ونوم و سال انتشار 2018 " +
            "و کارگردان روبن فلسچر و مدت زمان 120 دقیقه " +
            "و ژانر علمی تخیلی و رده سنی +18 " +
            "و قیمت اجاره روزانه 20 تومان " +
            "و نرخ جریمه 15 درصد و تعداد 1 عدد اضافه میکنم")]
        private async Task When()
        {
            var dto = new AddMovieDto()
            {
                Title = "Venom",
                Description = "dummy_description",
                PublishYear = "2018",
                Director = "Ruben Fleischer",
                Duration = 120,
                GenreId = _genre.Id,
                MinimumAllowedAge = 18,
                DailyRentalPrice = 20,
                PenaltyRates = 15,
                Count = 1,
            };
            await _sut.Add(dto);
        }
        [Then("تنها یک فیلم با مشخصات مذکور باید در فهرست فیلم ها وجود داشته باشد")]
        public void Then()
        {
            var actual = ReadContext.Movies.Single();
            actual.Title.Should().Be("Venom");
        }
        [Fact]
        public void Run()
        {
            Runner.RunScenario(
                _ => Given(),
                _ => When().Wait(),
                _ => Then());
        }
    }

}
