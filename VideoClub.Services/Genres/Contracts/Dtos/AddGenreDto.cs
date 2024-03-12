using System.ComponentModel.DataAnnotations;

namespace VideoClub.Services.Genres.Contracts.Dtos
{
    public class AddGenreDto
    {
        [Required,MaxLength(50)]
        public string Title { get; set; }       
        public DateTime test {  get; set; }
        public int test2 { get; set; }
    }
}
