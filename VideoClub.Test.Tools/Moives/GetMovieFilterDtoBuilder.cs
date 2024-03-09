using VideoClub.Services.Movies.Contracts.Dtos;

namespace VideoClub.Test.Tools.Moives
{
    public class GetMovieFilterDtoBuilder
    {
        private readonly GetMovieFilterDto _dto;
        public GetMovieFilterDtoBuilder()
        {
            _dto = new GetMovieFilterDto()
            {
                Title = "dummy-title",
            };
        }
        public GetMovieFilterDtoBuilder WithTitle(string title)
        {
            _dto.Title = title;
            return this;
        }
        public GetMovieFilterDto Build()
        {
            return _dto;
        }
    }
}
