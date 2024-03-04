using VideoClub.Services.Movies.Contracts.Dtos;

namespace VideoClub.Test.Tools.Moives
{
    public static class UpdateMovieDtoFactory
    {
        public static UpdateMovieDto Create(int genreId)
        {
            return new UpdateMovieDto()
            {
                Title = "updated-dummy-title",
                Description = "updated-dummy_description",
                PublishYear = "2024",
                Director = "updated-dummy_director",
                Duration = 90,
                GenreId = genreId,
                MinimumAllowedAge = 21,
                DailyRentalPrice = 20,
                PenaltyRates = 15,
                Count = 3,
            };
        }
    }
}
