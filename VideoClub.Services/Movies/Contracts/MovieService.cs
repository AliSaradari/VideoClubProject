using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Services.Movies.Contracts.Dtos;

namespace VideoClub.Services.Movies.Contracts
{
    public interface MovieService
    {
        Task<List<GetMovieDto>> Get(GetMovieFilterDto dto);
    }
}
