using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models
{
    public class Sessao
    {
        [Required]
        public int? FilmeId { get; set; }

        public virtual Filme Filme { get; set; }

        [Required]
        public int? CinemaId { get; set; }
        public virtual Cinema Cinema { get; set; }
    }
}