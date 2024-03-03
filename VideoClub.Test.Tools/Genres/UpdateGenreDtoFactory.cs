using VideoClub.Services.Genres.Contracts.Dtos;

namespace VideoClub.Test.Tools.Genres
{
    public static class UpdateGenreDtoFactory
    {
        public static UpdateGenreDto Create(string? title = null)
        {
            return new UpdateGenreDto()
            {
                Title = title ?? "updated-dummy-Title",
            };
        }
    }
}
