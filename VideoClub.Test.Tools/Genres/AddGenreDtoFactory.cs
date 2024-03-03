using VideoClub.Services.Genres.Contracts.Dtos;

namespace VideoClub.Test.Tools.Genres
{
    public static class AddGenreDtoFactory
    {
        public static AddGenreDto Create(string? title = null)
        {
            return new AddGenreDto()
            {
                Title = title ?? "dummy-title",
            };
        }
    }
}
