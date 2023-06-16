using AutoMapper;
using FilmesApi.Dtos;
using FilmesApi.Models;

namespace FilmesApi.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
        }
    }
}