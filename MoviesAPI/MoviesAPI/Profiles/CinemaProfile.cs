using AutoMapper;
using MoviesAPI.Data.Dtos.Cinema;
using MoviesAPI.Data.Dtos.Movie;
using MoviesAPI.Models;

namespace MoviesAPI.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaDto, Cinema>();
            CreateMap<UpdateCinemaDto, Cinema>().ReverseMap();
            CreateMap<Cinema, ReadCinemaDto>();
        }
    }
}
