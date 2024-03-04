using VideoClub.Entities.Movies;

namespace VideoClub.Entities.Genres
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Rate { get; set; } = 0;
        public List<Movie> Movies { get; set; }
    }
}
