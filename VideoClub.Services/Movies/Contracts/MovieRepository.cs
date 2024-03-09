﻿using VideoClub.Entities.Movies;
using VideoClub.Services.Movies.Contracts.Dtos;

namespace VideoClub.Services.Movies.Contracts
{
    public interface MovieRepository
    {
        void Add(Movie movie);
        void Delete(Movie movie);
        Movie FindById(int id);
        List<GetMovieDto> Get(GetMovieFilterDto dto);
        List<GetMovieManagerDto> ManagerGet(GetMovieManagerFilterDto dto);
    }
}
