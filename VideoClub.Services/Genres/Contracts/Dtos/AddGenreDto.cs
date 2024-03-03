using System.ComponentModel.DataAnnotations;

namespace VideoClub.Services.Genres.Contracts.Dtos
{
    public class AddGenreDto
    {
        [Required,MaxLength(50)]
        public string Title { get; set; }        
    }
}
