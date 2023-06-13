using AutoMapper;
using FilmesApi.Dtos;
using FilmesApi.Models;

namespace FilmesApi.Profiles;


public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<CreateCinemaDto, Cinema>();
        CreateMap<UpdateCinemaDto, Cinema>();
        CreateMap<Cinema, ReadCinemaDto>()
               .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => src.Endereco))
               .ForMember(dest => dest.Sessoes, opt => opt.MapFrom(src => src.Sessoes));
    }
}