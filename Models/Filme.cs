using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Models;

public class Filme
{

    [Key]
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O gênero é obrigatório")]
    [MaxLength(50, ErrorMessage = "O tamanho máximo de gênero é de 50 caracteres")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "A duração é obrigatória")]
    [Range(70, 250, ErrorMessage = "A duração deve ser entre 70 e 250 minutos")]
    public int Duracao { get; set; }


    public Filme(string titulo, string genero, int duracao)
    {
        this.Titulo = titulo;
        this.Genero = genero;
        this.Duracao = duracao;
    }
}