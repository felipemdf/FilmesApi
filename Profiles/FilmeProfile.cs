using AutoMapper;
using FilmesApi.Dtos;
using FilmesApi.Models;

namespace FilmesApi.Profiles;


public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filme>();
        CreateMap<UpdateFilmeDto, Filme>();
        CreateMap<Filme, UpdateFilmeDto>();
        CreateMap<Filme, ReadFilmeDto>()
            .ForMember(dest => dest.Sessoes, opt => opt.MapFrom(src => src.Sessoes));
    }
}