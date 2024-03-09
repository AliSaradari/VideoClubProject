using VideoClub.Services.Movies.Contracts.Dtos;

namespace VideoClub.Test.Tools.Moives
{
    public static class AddMovieDtoFactory
    {
        public static AddMovieDto Create(int genreId,string? title = null,int? count = null)
        {
            return new AddMovieDto()
            {
                Title = title ?? "dummy-title",
                Description = "dummy_description",
                PublishYear = "2002",
                Director = "dummy_director",
                Duration = 120,
                GenreId = genreId,
                MinimumAllowedAge = 18,
                DailyRentalPrice = 10,
                PenaltyRates = 20,
                Count = count ?? 1,
            };
        }
    }
}
