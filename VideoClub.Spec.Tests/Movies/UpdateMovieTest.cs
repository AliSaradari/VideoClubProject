using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entities.Genres;
using VideoClub.Entities.Movies;
using VideoClub.Services.Movies.Contracts;
using VideoClub.Test.Tools.Genres;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig.IntegrationTest;
using VideoClub.Test.Tools.Moives;
using VideoClub.Test.Tools.Infrastructure.DatabaseConfig;
using VideoClub.Contracts.Interfaces;
using VideoClub.Infrastructure;
using VideoClub.Services.Movies.Contracts.Dtos;
using FluentAssertions;
using Xunit;


namespace VideoClub.Spec.Tests.Movies
{

    [Scenario("ویرایش کردن فیلم")]
    [Story("",
        AsA = "مدیر کلاب",
        InOrderTo = "یک فیلم موجود در فهرست فیلم ها را ویرایش کنم",
        IWantTo = "تا اطلاعات فیلم ها صحیح و به روز باشد ")]
    public class UpdateMovieTest : BusinessIntegrationTest
    {
        private readonly MovieManagerService _sut;
        private Movie _movie;
        private Genre _genre;
        public UpdateMovieTest()
        {
            _sut = MovieManagerServiceFactory.Create(SetupContext);
        }
        [Given("یک ژانر در فهرست ژانر ها وجود دارد")]
        [And(" یک فیلم با عنوان ونوم و سال انتشار 2018 " +
            "و کارگردان روبن فلسچر و مدت زمان 120 دقیقه " +
            "و ژانر علمی تخیلی و رده سنی +18 " +
            "و قیمت اجاره روزانه 20 تومان " +
            "و نرخ جریمه 15 درصد و تعداد 1 عدد در فهرست فیلم ها وجود دارد")]

        public void Given()
        {
            _genre = new GenreBuilder()
                .Build();
            DbContext.Save(_genre);
            _movie = new Movie()
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
                CreateAt = new DateTime(2024, 04, 14),
            };
            DbContext.Save(_movie);

        }
        [When("فیلم مذکور را به فیلم با عنوان بدبویز فور لایف با کارگردانی ادیل و سال ساخت 2020 به مدت زمان 180 دقیقه ویرایش کنیم")]
        public async Task When()
        {
            var dto = new UpdateMovieDto()
            {
                Title = "BadBoysForLife",
                Description = "dummy_description",
                PublishYear = "2020",
                Director = "Adil El Arbi",
                Duration = 180,
                GenreId = _genre.Id,
                MinimumAllowedAge = 18,
                DailyRentalPrice = 20,
                PenaltyRates = 15,
                Count = 1,
            };
            await _sut.Update(_movie.Id, dto);
        }
        [Then("باید در فهرست فیلم ها فقط یک فیلم با مشخصات مذکور وجود داشته باشد")]
        public void Then()
        {
            var actual = ReadContext.Movies.Single();
            actual.Title.Should().Be("BadBoysForLife");
            actual.Description.Should().Be("dummy_description");
            actual.PublishYear.Should().Be("2020");
            actual.Director.Should().Be("Adil El Arbi");
            actual.Duration.Should().Be(180);
            actual.GenreId.Should().Be(_genre.Id);
            actual.MinimumAllowedAge.Should().Be(18);
            actual.DailyRentalPrice.Should().Be(20);
            actual.PenaltyRates.Should().Be(15);
            actual.Count.Should().Be(1);
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
