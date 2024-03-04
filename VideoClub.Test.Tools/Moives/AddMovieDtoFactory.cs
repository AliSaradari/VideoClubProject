using VideoClub.Services.Movies.Contracts.Dtos;

namespace VideoClub.Test.Tools.Moives
{
    public static class AddMovieDtoFactory
    {
        public static AddMovieDto Create(int genreId)
        {
            return new AddMovieDto()
            {
                Title = "dummy-title",
                Description = "dummy_description",
                PublishYear = "2002",
                Director = "dummy_director",
                Duration = 120,
                GenreId = genreId,
                MinimumAllowedAge = 18,
                DailyRentalPrice = 10,
                PenaltyRates = 20,
                Count = 1,
            };
        }
    }
}
