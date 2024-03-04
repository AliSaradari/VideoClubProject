using System.ComponentModel;

namespace VideoClub.Services.Genres.Contracts.Dtos
{
    public class GetGenreDto
    {
        public string Title { get; set; }
        [DefaultValue(0)]
        public float Rate { get; set; }
    }
}
