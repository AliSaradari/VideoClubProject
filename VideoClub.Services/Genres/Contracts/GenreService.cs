using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Services.Genres.Contracts.Dtos;

namespace VideoClub.Services.Genres.Contracts
{
    public interface GenreService
    {
        Task<List<GetGenreDto>> Get();
    }
}
