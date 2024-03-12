namespace VideoClub.Services.Movies.Contracts.Dtos
{
    public class GetMovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string PublishYear { get; set; }
        public string Director { get; set; }
        public int Duration { get; set; }
        public string GenreTitle { get; set; }
        public int MinimumAllowedAge { get; set; }
        public decimal DailyRentalPrice { get; set; }
        public decimal PenaltyRates { get; set; }
        public int Count { get; set; }
    }
}
