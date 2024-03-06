using VideoClub.Entities.Genres;

namespace VideoClub.Entities.Movies
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublishYear { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; }
        public int GenreId { get; set; }
        public int MinimumAllowedAge { get; set; }
        public decimal DailyRentalPrice { get; set; }
        public decimal PenaltyRates { get; set; }
        public int Count { get; set; }
        public Genre genre { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
