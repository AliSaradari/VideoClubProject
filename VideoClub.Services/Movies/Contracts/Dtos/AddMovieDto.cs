using System.ComponentModel.DataAnnotations;

namespace VideoClub.Services.Movies.Contracts.Dtos
{
    public class AddMovieDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string PublishYear { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public int GenreId { get; set; }
        [Required]
        public int MinimumAllowedAge { get; set; }
        [Required]
        public decimal DailyRentalPrice { get; set; }
        [Required]
        public decimal PenaltyRates { get; set; }
        [Required]
        public int Count { get; set; }

    }
}
