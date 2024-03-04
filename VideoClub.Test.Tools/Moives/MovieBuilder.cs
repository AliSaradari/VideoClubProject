using VideoClub.Entities.Movies;

namespace VideoClub.Test.Tools.Moives
{
    public class MovieBuilder
    {
        private readonly Movie _movie;
        public MovieBuilder(int genreId)
        {
            _movie = new Movie()
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
        public MovieBuilder WithTitle (string title)
        {
            _movie.Title = title;
            return this;
        }
        public MovieBuilder WithGenreId (int genreId)
        {
            _movie.GenreId = genreId;
            return this;
        }
        public Movie Build()
        {
            return _movie;
        }
    }
}
