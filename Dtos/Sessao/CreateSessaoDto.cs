using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Dtos
{
    public class CreateSessaoDto
    {
        public int FilmeId { get; set; }
        public int CinemaId { get; set; }
    }
}