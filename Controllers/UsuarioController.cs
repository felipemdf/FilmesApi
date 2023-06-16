using AutoMapper;
using FilmesApi.Dtos;
using FilmesApi.Models;
using FilmesApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{

    private UsuarioService _usuarioService;

    public UsuarioController(UsuarioService cadastroService)
    {
        _usuarioService = cadastroService;
    }

    [HttpPost]
    public async Task<IActionResult> CadastraUsuarioAsync(CreateUsuarioDto dto)
    {
        await _usuarioService.CadastraUsuario(dto);
        return Ok("Usuário cadastrado!");
    }

    [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginUsuarioDto dto)
        {
            var token = await _usuarioService.Login(dto);
            return Ok(token);
        }
}