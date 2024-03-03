using VideoClub.Entities.Genres;

namespace VideoClub.Test.Tools.Genres
{
    public class GenreBuilder
    {
        private readonly Genre _genre;
        public GenreBuilder()
        {
            _genre = new Genre()
            {
                Title = "dummy-title",
                Rate = 3
            };
        }
        public GenreBuilder WithTitle(string title)
        {
            _genre.Title = title;
            return this;
        }
        public Genre Build()
        {
            return _genre;
        }
    }
}
