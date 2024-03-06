using VideoClub.Entities.Movies;

namespace VideoClub.Entities.Genres
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
