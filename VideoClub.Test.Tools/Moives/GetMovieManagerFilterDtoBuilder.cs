using VideoClub.Services.Movies.Contracts.Dtos;

namespace VideoClub.Test.Tools.Moives
{
    public class GetMovieManagerFilterDtoBuilder
    {
        private readonly GetMovieManagerFilterDto _dto;
        public GetMovieManagerFilterDtoBuilder()
        {
            _dto = new GetMovieManagerFilterDto()
            {
                Title = null,
            };

        }
        public GetMovieManagerFilterDtoBuilder WithTitle(string title)
        {
            _dto.Title = title;
            return this;
        }
        public GetMovieManagerFilterDto Build()
        {
            return _dto;
        }
    }
}
