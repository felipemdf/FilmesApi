using Microsoft.AspNetCore.Identity;

namespace FilmesApi.Models;

public class Usuario : IdentityUser
{
    public DateTime DataNascimento { get; set; }
    public Usuario() : base() { }
}
