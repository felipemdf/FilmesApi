using FilmesApi.Models;
using FilmesApi.Dtos;
using FilmesApi.Data;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

namespace FilmesApi.Controllers;

/// <summary>
/// Controller responsável pelas operações relacionadas aos filmes
/// </summary>
[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private FilmeContext _context;
    private IMapper _mapper;

    /// <summary>
    /// Construtor da classe FilmesController
    /// </summary>
    /// <param name="context">Contexto do banco de dados</param>
    /// <param name="mapper">Instância do IMapper</param>
    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao banco de dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionaFilme(
        [FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePorId),
            new { id = filme.Id },
            filme);
    }

    /// <summary>
    /// Recupera uma lista de filmes do banco de dados
    /// </summary>
    /// <param name="skip">Quantidade de filmes a serem ignorados (opcional, padrão = 0)</param>
    /// <param name="take">Quantidade máxima de filmes a serem retornados (opcional, padrão = 5)</param>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Caso a consulta seja feita com sucesso</response>
    [HttpGet]
    [ProducesResponseType(200)]
    public IEnumerable<ReadFilmeDto> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 5, [FromQuery] string? nomeCinema = null)
    {
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take)
            .Where(filme => filme.Sessoes.Any(sessao => sessao.Cinema.Nome == nomeCinema)).ToList());
    }

    /// <summary>
    /// Recupera um filme pelo ID
    /// </summary>
    /// <param name="id">ID do filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso o filme seja encontrado</response>
    /// <response code="404">Caso o filme não seja encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null)
            return NotFound();

        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);

        return Ok(filmeDto);
    }

    /// <summary>
    /// Atualiza um filme pelo ID
    /// </summary>
    /// <param name="id">ID do filme</param>
    /// <param name="filmeDto">Objeto com os campos a serem atualizados</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso o filme seja atualizado com sucesso</response>
    /// <response code="404">Caso o filme não seja encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        // Filme filme = _mapper.Map<Filme>(filmeDto);

        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null)
        {
            return NotFound();
        }

        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Atualiza parcialmente um filme pelo ID
    /// </summary>
    /// <param name="id">ID do filme</param>
    /// <param name="patch">Patch contendo as alterações a serem aplicadas no filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a atualização parcial seja feita com sucesso</response>
    /// <response code="404">Caso o filme não seja encontrado</response>
    [HttpPatch("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult AtualizaFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        // Filme filme = _mapper.Map<Filme>(filmeDto);

        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null)
        {
            return NotFound();
        }

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(patch);
        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();

        return NoContent();
    }

    /// <summary>
    /// Deleta um filme pelo ID
    /// </summary>
    /// <param name="id">ID do filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso o filme seja deletado com sucesso</response>
    /// <response code="404">Caso o filme não seja encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

        if (filme == null)
            return NotFound();

        _context.Remove(filme);
        _context.SaveChanges();

        return NoContent();
    }
}
