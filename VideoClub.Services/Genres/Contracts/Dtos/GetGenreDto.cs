using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Services.Genres.Contracts.Dtos
{
    public class GetGenreDto
    {
        public string Title { get; set; }
        [DefaultValue(0)]
        public float Rate { get; set; }
    }
}
