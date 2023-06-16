using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FilmesApi.Models;

namespace FilmesApi.Services;

public class TokenService
{
    public string GenerateToken(Usuario usuario)
    {
        Claim[] claims = new Claim[]
        {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id),
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString()),
                new Claim("loginTimestamp", DateTime.UtcNow.ToString())
        };

        var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY_JWT") ?? "TmSfZhvGfJ9ru7b6QaVw6BG2MmEY7Lm3";
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
            (
            expires: DateTime.Now.AddMinutes(10),
            claims: claims,
            signingCredentials: signingCredentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
