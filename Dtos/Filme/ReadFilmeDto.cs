using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Dtos;

public class ReadFilmeDto
{

    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Genero { get; set; }
    public int Duracao { get; set; }

    public ICollection<ReadSessaoDto> Sessoes { get; set; }
    public DateTime horaDaConsulta {get; set;} = DateTime.Now;


    public ReadFilmeDto(string titulo, string genero, int duracao)
    {
        this.Titulo = titulo;
        this.Genero = genero;
        this.Duracao = duracao;
    }
}